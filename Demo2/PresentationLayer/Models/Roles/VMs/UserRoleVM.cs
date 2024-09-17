using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.Models.Roles.VMs
{
    public class UserRoleVM
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
    }
}
