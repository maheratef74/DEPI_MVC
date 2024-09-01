using BusinessLayer.DTOs;
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

        Task AddProduct(CreateProductDto productDto);

        Task UpdateProduct(UpdateProductDto productDto);
    }
}
