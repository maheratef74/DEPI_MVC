using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Api.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/Product")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null) 
                return NotFound();

            return Ok(product);
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
        [HttpGet("department/{department?}")]
        public async Task<IActionResult> GetProductsByCategory(string? department = null)
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
