using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Api.ActionRequests;

namespace PresentationLayer.Api.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/Product")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _config;

        public ProductController(IProductService productService, IConfiguration config)
        {
            _productService = productService;
            _config = config;
        }

        // 🚩🚩 /api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        // 🚩🚩 /api/products/1
        // 🚩🚩 /api/products/2
        // 🚩🚩 /api/products/3
        //[Route("{id}")]
        [HttpGet("{id:int}", Name = "FindProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            //try
            //{
                var product = await _productService.GetById(id);

                if (product == null)
                    return NotFound();

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
