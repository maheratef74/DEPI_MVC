using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly IDepartmentRepository _departmentRepository;

        public ProductController(IProductService productService, IFileService fileService, IDepartmentRepository departmentRepository)
        {
         _productService = productService;
            _fileService = fileService;
            _departmentRepository = departmentRepository;
        }
        // 1) Catch Request

        public int Add(int a , int b)
        {
            _productService.GetById(1);
            return a + b;
        }
        // 🚀/product/index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 2) Call Model
            // ProductSampleData productBL = new ProductSampleData();
            // 3) Model returns Data
            // List<Product> products = productBL.GetAll();

            List<GetAllProductsDTO> products = await _productService.GetAll();
            // Add(1, 2);
            // 4) Controller sends data to view

            var productsList = products
                .Select(productDto => (GetAllProductsVM)productDto)
                .ToList();

            return View(productsList);

        }

        // 🚀/product/details/:id    ✅  path variable
        // 🚀/product/details?id=✔️  ✅  query parameters
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // 2) Call Model
            // ProductSampleData productBL = new ProductSampleData();
            // 3) Model returns Data
            // List<Product> products = productBL.GetById(id);
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            var productVM = (GetProductDetailsVM)product;
            // 4) Controller sends data to view
            // return View(product); 
            return View("Details", productVM);
        }

        //  /product/create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = departments.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductActionRequest product)
        {
            if (product.DepartmentId == 0)
            {
                ModelState.AddModelError("DepartmentId", "Department Id is not valid");
            }

            if(ModelState.IsValid)
            {
                string uniqueFileName = _fileService.UploadFile(product.Image, "Images");

                // 2- CreateProductActionRequest ( PL )   ➡️➡️  CreateProductDto ( BL )

                var productDto = new CreateProductDto
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    DepartmentId = product.DepartmentId,
                    Image = uniqueFileName
                };

                // 3- Call ProductService.AddProduct(CreateProductDto)

                _productService.AddProduct(productDto);

                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = departments.ToList();

            return View(product);
        }


        //  /product/update/:id
        //  /product/update?id=✅
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
           var product = await _productService.GetById(id);

            var productActionRequest = new UpdateProductActionRequest
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                DepartmentId = product.DepartmentId,
                Description = product.Description,
            };

            return View(productActionRequest);
        }
        [HttpPost]
        public IActionResult Update(UpdateProductActionRequest productActionRequest)
        {
            // 2- UpdateProductActionRequest ( PL )   ➡️➡️  UpdateProductDto ( BL )

            var udpateProductDto = new UpdateProductDto
            {
                Id = productActionRequest.Id,
                Name = productActionRequest.Name,
                Price = productActionRequest.Price,
                DepartmentId = productActionRequest.DepartmentId,
                Description = productActionRequest.Description,
            };
            // 3- Call ProductService.AddProduct(CreateProductDto)

            _productService.UpdateProduct(udpateProductDto);

            return RedirectToAction(nameof(Details), new { id = productActionRequest.Id});
        }
    }
}
