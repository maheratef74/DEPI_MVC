using BusinessLayer.DTOs;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IProductService
    {
        Task<List<GetAllProductsDTO>> GetAll();

        Task<GetProductDetailsDTO?> GetById(int id);

        Task<Product> AddProduct(CreateProductDto productDto);

        Task UpdateProduct(UpdateProductDto productDto);

        Product? GetProductByName(string name);
    }
}
