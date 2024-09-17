using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models.Roles.VMs;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult AssignUserRole()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            var model = new UserRoleVM
            {
                Users = users.Select(u => new SelectListItem { Value = u.Id, Text = u.UserName }).ToList(),
                Roles = roles.Select(r => new SelectListItem { Value = r.Id, Text = r.Name }).ToList()
            };


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignUserRole(UserRoleVM request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (user == null || role == null)
            {
                ModelState.AddModelError("Invalid Data", "Invalid User or Role");
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                TempData["SucessMessage"] = "USer has been successfully assigned to the role";
                return RedirectToAction("AssignUserRole");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View("AssignUserRole",request);
        }
    }
}
