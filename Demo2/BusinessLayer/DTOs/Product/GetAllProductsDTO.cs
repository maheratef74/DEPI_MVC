using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class GetAllProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }

        public static explicit operator GetAllProductsDTO(Product product)
        {
            return new GetAllProductsDTO
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
