using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Order.ActionRequest;
using PresentationLayer.Models.Order.VM;

namespace PresentationLayer.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, ICustomerService customerService, IProductService productService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var customers = await _customerService.GetAll();
            var products = await _productService.GetAll();
            var vm = new CreateOrderVM
            {
                Customers = customers,
                Products = products
            };


            var myCustomers = new List<string>
            {
                "A",
                "B",
                "C",
            };
            ViewBag.myCustomers = myCustomers;

            ViewData["myCustomers"] = myCustomers;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderActionRequest request)
        {
            await _orderService.CreateOrder(request.ToDto());
            return RedirectToAction("Index");
        }

        // /Product/Update/2
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            // Get Order
            var order = await _orderService.GetOrderById(id);
            return View(order);
        }

        // /Product/Update/3
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateOrderActionRequest request)
        {
            // Update Order
            var updateOrderDto = request.ToDto();
            updateOrderDto.Id = id;
            await _orderService.UpdateOrder(updateOrderDto);
            //return RedirectToAction("Index","Product");
            return RedirectToAction("Update","Order", new { id = id });
        }

    }
}
