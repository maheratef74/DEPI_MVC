using BusinessLayer.DTOs;

namespace PresentationLayer.Models.Order.VM
{
    public class CreateOrderVM
    {
        public List<GetAllCustomerDto> Customers { get; set; }
        public List<GetAllProductsDTO> Products { get; set; }
    }
}
