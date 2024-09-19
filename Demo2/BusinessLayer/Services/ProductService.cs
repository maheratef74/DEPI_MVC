using BusinessLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        // Dependency Injection
        // Constructor Injection
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<List<GetAllProductsDTO>> GetAll()
        {
            var products = await  _productRepository.GetAll();

            // Imperative
            //var productList = new List<GetAllProductsDTO>();
            //foreach (var product in products)
            //{
            //    productList.Add((GetAllProductsDTO)product);
            //}

            // Declarative
            var productList = products
                .Select(product => (GetAllProductsDTO)product)
                .ToList();

            return productList;
        }
        public async  Task<GetProductDetailsDTO?> GetById(int id)
        {
            var product = await  _productRepository.GetProductById(id);
            if (product != null)
            {
                var productDto = product.ToDto();
                return productDto;
            }
            return null;

        }
        public async Task AddProduct(CreateProductDto productDto)
        {
            var maxId = await _productRepository.GetMaxId();

            // CreateProductDto(BL)   ➡️➡️  Product(DAL)

            var product = new Product()
            {
                Id = maxId + 1,
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                DepartmentId = productDto.DepartmentId,
                Image = productDto.Image
            };

            _productRepository.AddProduct(product);
            //await _productRepository.AddProduct_Concurrency_Async();
        }
        public async Task UpdateProduct(UpdateProductDto productDto)
        {
            var product = new Product()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                DepartmentId = productDto.DepartmentId,
            };

            await _productRepository.Update(product);
        }

        public Product? GetProductByName(string name)
        {
            return _productRepository.FindProductByName(name);
        }
    }
}
