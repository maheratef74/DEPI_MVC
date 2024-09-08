using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task Delete(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(order => order.Id == id);

            if (order != null)
            {
                _dbContext.Orders.Remove(order);
            }
        }

        public async Task<List<Order>> GetAll()
        {
           return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<int> GetMaxId()
        {
            return await _dbContext.Orders.MaxAsync(order => order.Id);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Order updatedOrder)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(order => order.Id == updatedOrder.Id);

            if (order != null)
            {
                order.Review = updatedOrder.Review;
                order.Rating = updatedOrder.Rating;
            }

            // _dbContext.Orders.Update(updatedOrder);
            // Update Orders Set    where id = updatedOrder.Id
        }
    }
}
