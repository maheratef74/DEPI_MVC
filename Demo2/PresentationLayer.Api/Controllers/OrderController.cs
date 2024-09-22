using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Api.ActionRequests;

namespace PresentationLayer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderActionRequest request)
        {
            await _orderService.UpdateOrder(request.ToDto());
            return NoContent();
        }
    }

    
}
