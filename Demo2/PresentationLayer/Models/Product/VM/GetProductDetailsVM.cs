using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace PresentationLayer.Models
{
    public class GetProductDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }

        public static explicit operator GetProductDetailsVM(GetProductDetailsDTO product)
        {
            return new GetProductDetailsVM
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
