using BusinessLayer.DTOs;
using DataAccessLayer.Repositories;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task CreateOrder(CreateOrderDto createOrderDto)
        {
            var order = createOrderDto.ToOrder();

            // 1- Insert Order ( Parent )
            await _orderRepository.AddOrder(order); // ➡️➡️ Change Tracker tracks your new order

            // 🚩🚩 order.Id = default = 0

            // Commit ( Save Changes ) ➡️➡️ Execution of Sql Query ( Insert into Orders )

            await _orderRepository.SaveChanges();
            // 🚩🚩 order.Id = 3

            // 2- Insert OrderProducts ( Child )
            order.OrderProducts = createOrderDto.ProductIds
                .Select(productId => new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Amount = 1,
                })
                .ToList();

            await _orderRepository.SaveChanges();
        }

        public async Task<OrderDetailsDto> GetOrderById(int id)
        {
            var order = await _orderRepository.GetById(id);
            var orderDto = new OrderDetailsDto()
            {
                Id = order.Id,
                Rating = order.Rating,
                Review = order.Review,
                OrderProducts = order.OrderProducts
            };
            return orderDto;
        }

        public async Task UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            // Get Entity from DB
            var order = await _orderRepository.GetById(updateOrderDto.Id);
            // Update the Entity Properties
            order.Review = updateOrderDto.Review;
            order.Rating = updateOrderDto.Rating;
            // Save Changes
            await _orderRepository.SaveChanges();
        }
    }
}
