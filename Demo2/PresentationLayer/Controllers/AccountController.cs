using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Licenses;
using PresentationLayer.Models.User;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Register(RegisterUserActionRequest newAccount)
        {
            //Sara*DEPI9
            //Maher*DEPI9
            //Ibrahim*DEPI9
            if (ModelState.IsValid)
            {
                // Create new Account
                var user = new User
                {
                    UserName = newAccount.UserName,
                    PasswordHash = newAccount.Password,
                    Address = newAccount.Address
                };



                // Hashing Algorithm

                // 123 ===> abc
                // 123xyz  ==> jki

                // _userManager.CreateAsync(user);
                // ➡️➡️ this overloads doesnot hash the password ,
                // doesnot treat it as password and doesnot validate it unless you write custom validation options
                // while registering identity in program.cs

                IdentityResult result = await _userManager.CreateAsync(user, newAccount.Password); // ==> save in database

                if (result.Succeeded) // User saved succesfully to database
                {
                    // Create a Cookie
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View(newAccount);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginActionRequest request)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user != null)
                {
                    var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

                    if (isPasswordValid)
                    {
                        // Create a Cookie
                        await _signInManager.SignInAsync(user, request.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("Invalid Credentials", "Username or Password invalid");
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
