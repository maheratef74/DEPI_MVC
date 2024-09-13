using BusinessLayer.DTOs;

namespace PresentationLayer.Models.Order.ActionRequest
{
    public class CreateOrderActionRequest
    {
        public int CustomerId { get; set; }
        public Dictionary<int, int> ProductAmounts { get; set; }
        public CreateOrderDto ToDto()
        {
            return new CreateOrderDto
            {
                CustomerId = CustomerId,
                ProductAmounts = ProductAmounts
            };
        }
    }
}
