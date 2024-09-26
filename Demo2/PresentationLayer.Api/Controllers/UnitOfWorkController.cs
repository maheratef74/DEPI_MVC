using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities;

namespace PresentationLayer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfWorkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _unitOfWork.ProductRepository.AddProduct(
                new Product
                {
                    Id = 10,
                    Name = "My Product",
                    Price = 55000,
                    Description = "Perfect Product",
                    Image = "1.jpg",
                    DepartmentId = 2
                });

            await _unitOfWork.OrderRepository.Add(
                new Order
                {
                    Review = "Good",
                    Rating = 5,
                    CustomerId = 1
                });


            await _unitOfWork.SaveChanges();

            return Ok();
        }
    }
}
