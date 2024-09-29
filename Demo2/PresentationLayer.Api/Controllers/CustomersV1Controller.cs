using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Api.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/customers")]
    //[Route("api/customers")]
    [ApiController]

    //  /api/v1/customers
    public class CustomersV1Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Response from Api Version 1");
        }
    }
}
