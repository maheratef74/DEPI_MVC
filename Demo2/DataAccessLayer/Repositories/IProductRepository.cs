using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<int> GetMaxId();
        Task AddProduct(Product product);
        Task Update(Product updatedProduct);
        Task Delete(int id);
        Task SaveChanges();
        Task<Product> GetProductByIdIncludingDepartment(int id);
        //public void AddProduct_Concurrency();
        //public Task AddProduct_Concurrency_Async();
    }
}
