using DataAccessLayer.Entities;

namespace BusinessLayer.DTOs
{
    public class CreateOrderDto
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
    }
    public static class CreateOrderDtoExtensions
    {
        public static Order ToOrder(this CreateOrderDto dto)
        {
            return new Order
            {
                Rating = default,
                Review = default,
                OrderProducts = new List<OrderProduct>(),
                Date = DateTime.Now,
                CustomerId = dto.CustomerId
            };
        }
    }
}
