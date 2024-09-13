﻿using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products
                .Where(product => true)
                .ToListAsync();  // execute query
            // Select * from Products
        }
        public async Task<Product?> GetById(int id)
        {
            // Linq
            return await _dbContext.Products
                .FirstOrDefaultAsync(product => product.Id == id); // execute query
            // Select * from Products Where Id = id 
        }
        public async Task<int> GetMaxId()
        {
            // input   ===>   output
            // Func<Input,Output>
            return await _dbContext.Products
                .MaxAsync(product => product.Id);
        }
        public async Task AddProduct(Product product)
        {
            await _dbContext.Products
                .AddAsync(product);
        }

        public async Task Update(Product updatedProduct)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(product => product.Id == updatedProduct.Id);

            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                // product.Image = updatedProduct.Image;
                product.DepartmentId = updatedProduct.DepartmentId;
            }

            // _dbContext.Products.Update(updatedProduct);
            // Update Products Set    where id = updatedProduct.Id
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _dbContext.Products
                    .FirstOrDefaultAsync(product => product.Id == id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            // Lazy Loading
            //return await _dbContext.Products
            //    .FirstOrDefaultAsync(product => product.Id == id);
            // Eager Loading
            return await _dbContext.Products
                .Include(product => product.Department)
                .Include(product => product.OrderProducts)
                .FirstOrDefaultAsync(product => product.Id == id); // execute query
            // Select * from Products Where Id = id 
        }

        public Product? FindProductByName(string name)
        {
            return _dbContext.Products.FirstOrDefault(product => product.Name == name);
        }


        #region Concurrency
        //public void AddProduct_Concurrency()
        //{
        //    var task1 = Task.Factory.StartNew(() => AddProduct1());
        //    var task2 = Task.Factory.StartNew(() => AddProduct2());

        //    Task
        //        .WhenAll(task1, task2)
        //        .ContinueWith(t => Console.WriteLine("Finished Saving both Concurrently!"));
        //}
        //public void AddProduct1()
        //{
        //    var product = new Product
        //    {
        //        Id = 5,
        //        Name = "Iphone 6s",
        //        Description = "Pro Max",
        //        Image = "3.jpg",
        //        Price = 50_000
        //    };
        //    _dbContext.Products.Add(product);
        //    _dbContext.SaveChanges();

        //}
        //public void AddProduct2()
        //{
        //    var product = new Product
        //    {
        //        Id = 5,
        //        Name = "Iphone 7",
        //        Description = "Pro",
        //        Image = "4.jpg",
        //        Price = 60_000
        //    };
        //    _dbContext.Products.Add(product);
        //    _dbContext.SaveChanges();
        //}
        #endregion

        #region Concurrency_Async
        //public async Task AddProduct_Concurrency_Async()
        //{
        //    var task1 = AddProduct1_Async();
        //    var task2 = AddProduct2_Async();

        //    Task
        //        .WhenAll(task1, task2)
        //        .ContinueWith(t => Console.WriteLine("Finished Saving both Concurrently!"));
        //}
        //public async Task AddProduct1_Async()
        //{
        //    var product = new Product
        //    {
        //        Id = 5,
        //        Name = "Iphone 6s",
        //        Description = "Pro Max",
        //        Image = "3.jpg",
        //        Price = 50_000
        //    };
        //    await _dbContext.Products.AddAsync(product);
        //    await _dbContext.SaveChangesAsync();

        //}
        //public async Task AddProduct2_Async()
        //{
        //    var product = new Product
        //    {
        //        Id = 5,
        //        Name = "Iphone 7",
        //        Description = "Pro",
        //        Image = "4.jpg",
        //        Price = 60_000
        //    };
        //    await _dbContext.Products.AddAsync(product);
        //    await _dbContext.SaveChangesAsync();
        //}
        #endregion
    }
}
