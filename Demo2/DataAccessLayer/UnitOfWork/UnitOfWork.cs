using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IProductRepository ProductRepository { get; }

        public ICustomerRepository CustomerRepository { get; }

        public IGenericRepository<Order> OrderRepository { get; }

        public UnitOfWork
        (
            ApplicationDbContext dbContext,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IGenericRepository<Order> orderRepository
        )
        {
            _dbContext = dbContext;
            ProductRepository = productRepository;
            CustomerRepository = customerRepository;
            OrderRepository = orderRepository;
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
