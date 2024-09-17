using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.User
{
    public class LoginActionRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
