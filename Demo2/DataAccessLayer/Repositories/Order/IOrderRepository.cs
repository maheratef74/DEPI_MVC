using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
        Task<Order?> GetById(int id);
        Task<int> GetMaxId();
        Task AddOrder(Order order);
        Task Update(Order updatedOrder);
        Task Delete(int id);
        Task SaveChanges();
    }
}
