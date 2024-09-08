using BusinessLayer.DTOs;

namespace PresentationLayer.Models.Order.ActionRequest
{
    public class CreateOrderActionRequest
    {
        public int customerId { get; set; }
        public List<int> productIds { get; set; }
        public CreateOrderDto ToDto()
        {
            return new CreateOrderDto
            {
                CustomerId = customerId,
                ProductIds = productIds
            };
        }
    }
}
