using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Queries;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.GenericProduct;
using DataAccessLayer.SP_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PresentationLayer.Api.ActionRequests;
using PresentationLayer.Api.Filters;
using Serilog;
using System.Security.Claims;
using System.Text.Json;

namespace PresentationLayer.Api.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/Product")]
    //[Route("api/[controller]/[action]")]+
    //[Authorize]
    [ApiController]
    //[ServiceFilter(typeof(LogExecutionTimeFilterAsync))]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;
        private readonly IGenericProductRepository _genericProductRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IDistributedCache _cache;

        public ProductController(IProductService productService, IConfiguration config, IProductRepository productRepository, IGenericProductRepository genericProductRepository, ILogger<ProductController> logger, IDistributedCache cache)
        {
            _productService = productService;
            _config = config;
            _productRepository = productRepository;
           _genericProductRepository = genericProductRepository;
            _logger = logger;
            _cache = cache;
        }

        // 🚩🚩 /api/products
        [HttpGet]
        //[Authorize]
        //[ServiceFilter(typeof(IPWhiteListAuthorizationFilter))]
        [ServiceFilter(typeof(RedisCacheResourceFilter))]
        public async Task<IActionResult> GetAllProducts()
        {
            //try
            //{
            //throw new Exception("Hiiiiiiiiiiiiiiii");
            _logger.LogInformation("Getting List Of Products");
            var products = await _productService.GetAll();
            //await Task.Delay(1000);
            return Ok(products);
            //}
            //catch (Exception ex)
            //{

            //    //_logger.LogError(ex.Message);
            //    return BadRequest(ex.Message);
            //}

        }

        // 🚩🚩 /api/products/1
        // 🚩🚩 /api/products/2
        // 🚩🚩 /api/products/3
        //[Route("{id}")]
        [HttpGet("{id:int}", Name = "FindProductById")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProductById(int id)
        {
            //try
            //{
            var cacheKey = $"Product_{id}_";
            string serializedProduct;
            serializedProduct = await _cache.GetStringAsync(cacheKey);

            if(!string.IsNullOrWhiteSpace(serializedProduct))
            {
                var cachedProduct = JsonSerializer.Deserialize<Product>(serializedProduct);
                return Ok(cachedProduct);
            }

            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound();

            serializedProduct = JsonSerializer.Serialize(product);
            await _cache.SetStringAsync(cacheKey, serializedProduct, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

            return Ok(product);
            //}
            //catch (Exception ex)
            //{

            //    return StatusCode(StatusCodes.Status500InternalServerError, "Application crashed");
            //}
           
        }


        // 🚩🚩 /api/products/1
        //[HttpPut("{id}")]
        [HttpPut("{id:int}")] // Route Constrain
        public IActionResult UpdateProduct([FromRoute] int id,[FromForm] Person person)
        {
            return Ok();
        }

        // 🚩🚩 /api/products/fruits
        [HttpPost("fruits")]
        public IActionResult AddFruits([FromForm] string[] fruits)
        {
            return Ok();
        }


        // 🚩🚩 /api/product/test
        [HttpGet("test")]
        public IActionResult Test([FromQuery] int age, [FromHeader] string jobtitle)
        {
            return Ok();
        }


        // 🚩🚩 /api/product/{department}
        [HttpGet("department/{department:alpha}")]
        //[HttpGet("{department?}")]
        public async Task<IActionResult> GetProductsByDeparment(string? department = null)
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        // 🚩🚩 /api/product/1
        // 🚩🚩 /api/product/2
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            return NoContent();
            return StatusCode(204, "Product Deleted");
            return StatusCode(StatusCodes.Status204NoContent, "Product Deleted");
        }

        // ➡️➡️ /api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductActionRequest request)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUserRoleClaims = User.FindAll(ClaimTypes.Role);
            #region Built-in Functionality in Asp .Net Core Api Controller
            // Built-in Functionality in Asp .Net Core Api Controller

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //} 
            #endregion

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;


            if (request.Image != null && request.Image.Length > 0)
            {
                
                string filePath = Path.Combine(@"./Images/", uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    request.Image.CopyTo(fileStream);
                }
            }

            CreateProductDto productDto = new CreateProductDto()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Image = uniqueFileName,
                DepartmentId = request.DepartmentId,
            };

            var product = await _productService.AddProduct(productDto);

            // Add Location Header to the response
            //================================================
            // Location : http://localhost:5044/product/{product.Id}

            //return CreatedAtAction(nameof(GetProductById), new { id = product.Id });
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDto);

            //var domain = _config["Domain"];
            //var url = $"http://localhost:5044/product/{product.Id}";
            //var url = $"domain/{product.Id}";
            //var url = Url.Link("FindProductById", new { id = product.Id });
            //return Created(url, productDto);
        }



        [HttpGet("paginated")]
        public async Task<ActionResult<PagedList<Product>>> GetPaginatedProducts([FromQuery] ProductQueryParameters parameters)
        {
            //var products = await _productRepository.GetProductsAsync(parameters);
            //var products = await _productRepository.GetProductsAsync(parameters);
            var products = await _genericProductRepository.ListAllAsync(p => p.Price, 1, 10, p => p.Department);
            return Ok(products);
        }

        [HttpGet("from-stored-proc")]
        public async Task<ActionResult<PagedList<SP_GetProducts_Model>>> GetProducts_StoredProc(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string sortBy = "Name", 
            [FromQuery] string searchTerm = ""
        )
        {
            //var products = await _productRepository.GetProductsAsync(parameters);
            //var products = await _productRepository.GetProductsAsync(parameters);
            var products = await _genericProductRepository.Get_StoredProc(pageNumber, pageSize, sortBy, searchTerm);
            return Ok(products);
        }
    }   
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}
