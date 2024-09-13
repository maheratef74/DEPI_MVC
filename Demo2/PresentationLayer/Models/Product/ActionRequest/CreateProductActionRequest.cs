using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Validators;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class CreateProductActionRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        //[UniqueNameValidator]
        [Remote(action: "CheckName", controller: "Product", ErrorMessage = "Name is used before")]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [EmailAddress] // -----@-------
        [MyEmailDomain("advsys.com","microsoft.com")]
        public string Email { get; set; }
        [RegularExpression("[^(a-zA-Z0-9_)]")]
        public string Password { get; set; }
    }
}
