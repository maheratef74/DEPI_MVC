using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Roles.ActionRequests;

namespace PresentationLayer.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleActionRequest request)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = request.RoleName;
                IdentityResult result = await _roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(request);
        }
    }
}
