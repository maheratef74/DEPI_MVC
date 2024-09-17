using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Roles.ActionRequests
{
    public class CreateRoleActionRequest
    {
        [Required]
        public string RoleName { get; set; }
    }
}
