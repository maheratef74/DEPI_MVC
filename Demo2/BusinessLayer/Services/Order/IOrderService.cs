using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto createOrderDto);
    }
}
