using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Product.VM;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login([FromQuery] string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        //       /Product/Index
        //  Redirection ➡️ /Account/Login?returnUrl=/Product/Index
        //  Redirection ➡️ /Account/Login?returnUrl=/Product/Details/1
        //  Redirection ➡️ /Account/Login?returnUrl=/Product/Edit/1
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM request, string? ReturnUrl)
        {
            // HttpContext.User  ➡️➡️ app.UseAuthentication()  fill this property with its value using the cookie coming in the http request

            // HttpContext.User  ➡️➡️ Claim Principal

            // Claim Principal ➡️➡️ Group of Claim Identities ➡️➡️ Group of Claims


            if (ModelState.IsValid)
            {
                // Check in Database
                var user = MyUsers.FirstOrDefault(user => user.Name == request.Name);
                if (user != null)
                {
                    Claim c1 = new Claim(ClaimTypes.Name, user.Name);
                    Claim c2 = new Claim(ClaimTypes.Email, user.Email);
                    Claim c3 = new Claim(ClaimTypes.Role, user.Role);

                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    ci.AddClaim(c1);
                    ci.AddClaim(c2);
                    ci.AddClaim(c3);

                    ClaimsPrincipal cp = new ClaimsPrincipal(ci);

                    await HttpContext.SignInAsync(cp);

                    // 🚩🚩 Creates a cookie:
                    //      It serializes the user's identity (ClaimsPrincipal) into an encrypted cookie
                    //      and stores it on the client (user's browser).  ➡️➡️ // Response.Cookies.Append('user', 'skdokd');

                    // 🚩🚩 Manages Authentication State:
                    //      This cookie is sent back to the server with each subsequent request,
                    //      allowing the server to identify the user and their roles without requiring them to log in again for each request.

                    // How the Flow Works After Sign-In
                    //=================================================
                    // 1- User Request: The user makes a request(e.g., accesses a secure page).

                    // 2- Authentication Middleware(app.UseAuthentication()): This middleware checks for the presence of an authentication cookie.

                    // 3- Cookie Validation: If a cookie is present, it is validated.If valid, the user's identity is reconstructed from the claims stored in the cookie.

                    // 4- User Is Authenticated: The authenticated user's information is made available via HttpContext.User, which is accessible across the entire request pipeline.

                    // 5- Authorization(if app.UseAuthorization() is also configured): If the application uses app.UseAuthorization(), it will then check whether the authenticated user is authorized to access the requested resource.

                    if(ReturnUrl != null)
                    {
                        return LocalRedirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Invalid Credentials", "Invalid Username or Password");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        public static List<UserInfo> MyUsers = new()
        {
            new UserInfo("Ahmed", "ahmed@gmail.com", UserRole.Admin),
            new UserInfo("Ali", "ali@gmail.com", UserRole.Customer),
            new UserInfo("Sara", "sara@gmail.com", UserRole.Buyer),
        };

    }

    public record UserInfo(string Name, string Email, string Role);

    public static class UserRole
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Buyer = "Buyer";
    }
}
