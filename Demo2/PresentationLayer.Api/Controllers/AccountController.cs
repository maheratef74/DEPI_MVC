using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PresentationLayer.Api.ActionRequests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PresentationLayer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        // 🚩🚩 Register ==> Create a new User
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserActionRequest request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Address = request.Address,
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return Ok("Account Registered");
            }

            
            var errors = result.Errors.Select(error => error.Description).ToArray();

            return BadRequest(errors);

        }
        // 🚩🚩 Login ==> Check Credentials valid and Create token
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserActionRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if(user != null) // user exists
            {
                // Create Claims
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // Add User Roles to Claims

                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                // Create Signing Signature = Secret + Hmac Algorithm

                var secret = _config["Jwt:Secret"];
                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));


                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Create jwt Token
                // Header.Payload.Signature

                JwtSecurityToken jwtToken = new JwtSecurityToken
                (
                    //issuer: "http://localhost:5044",
                    issuer: _config["Jwt:Issuer"],

                    //audience: "http://localhost:4200"

                    audience: _config["Jwt:Audience"],

                    claims: claims,

                    expires: DateTime.Now.AddHours(3),

                    signingCredentials: signingCredentials
                );

                return Ok
                (
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo,
                    }
                );
            }

            return Unauthorized();
        }
    }
}
