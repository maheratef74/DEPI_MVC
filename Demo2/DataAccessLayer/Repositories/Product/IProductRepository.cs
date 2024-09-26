using DataAccessLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Queries;
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
        Task<Product> GetProductById(int id);
        Product? FindProductByName(string name);
        //public void AddProduct_Concurrency();
        //public Task AddProduct_Concurrency_Async();
        Task<PagedList<Product>> GetProductsAsync(ProductQueryParameters parameters);
    }
}
