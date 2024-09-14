using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Product.VM
{
    public class LoginVM
    {
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
