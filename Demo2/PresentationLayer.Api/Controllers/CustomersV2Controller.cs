using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Api.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/customers")]
    //[Route("api/customers")]
    [ApiController]

    //  /api/v2/customers
    public class CustomersV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return Ok("Response from Api Version 2");
            return Ok(new { Version = "2.0", Message = "Response from Api Version 2" });
        }
    }
}
