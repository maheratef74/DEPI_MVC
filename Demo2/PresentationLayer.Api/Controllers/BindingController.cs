using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOne(int id, string name) // Primitive Types ==> Query Parameters
        {
            return Ok($"Id = {id}, Name = {name}");
        }

        [HttpGet("{id:int}/{name:alpha}")]
        public IActionResult GetTwo(int id, string name)
        {
            return Ok($"Id = {id}, Name = {name}");
        }
        [HttpPost]
        public IActionResult PostOne([FromForm] Instructor instructor)
        {
            return Ok($"Id = {instructor.Id}, Name = {instructor.Name}");
        }
        [HttpPost("{id:int}/{name:alpha}")]
        public IActionResult PostTwo(int id, string name, [FromForm] Instructor instructor)
        {
            return Ok($"Id = {id}, Name = {name} \nInstructor Id = {instructor.Id}, Instryctor Name = {instructor.Name}");
        }
        [HttpGet("methodinjection/{id:int}")]
        public async Task<IActionResult> UseMethodInjection(int id , [FromServices] IProductService productService)
        {
            return Ok(await productService.GetById(id));
        }

        [HttpGet("useheader")]
        public async Task<IActionResult> UseHeader([FromHeader]int id)
        {
            return Ok(id);
        }

        [HttpGet("complex/{id:int}/{name:alpha}")]
        public async Task<IActionResult> ComplexFromRoute([FromRoute] Instructor instructor)
        {
            return Ok($"Id = {instructor.Id}, Name = {instructor.Name}");
        }
    }
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
