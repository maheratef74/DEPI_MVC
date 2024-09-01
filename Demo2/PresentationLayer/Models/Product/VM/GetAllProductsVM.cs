using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace PresentationLayer.Models
{
    public class GetAllProductsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }

        public static explicit operator GetAllProductsVM(GetAllProductsDTO product)
        {
            return new GetAllProductsVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                DepartmentId = product.DepartmentId,
            };
        }
    }
}
