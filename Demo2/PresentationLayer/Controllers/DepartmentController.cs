using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models.Department.VMs;
using PresentationLayer.Models.Product.VM;

namespace PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _dbContext.Departments

                // Projection ( Select some column to bring from db )

                .Select(dep => new DepartmentVM
                {
                    Id = dep.Id,
                    Name = dep.Name,
                })

                .ToListAsync();
               

            return View(departments);
        }
        [HttpGet]
        public async Task<IActionResult> Products(int deptId)
        {
            var products = await _dbContext.Products
                .Where(product => product.DepartmentId == deptId)
                .Select(prod => new ProductListVM
                {
                    Id = prod.Id,
                    Name = prod.Name,
                })
                .ToListAsync();

            return Json(products);
        }
    }
}
