using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Client.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ClientUser> _userManager;
        private readonly SignInManager<ClientUser> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        //public IActionResult Index()
        //{
        //    ViewData["UserID"] = _userManager.GetUserId(this.User);
        //    return View();
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ClientUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Optionally sign in the user immediately after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Ok(new { Message = "Registration successful" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(ModelState);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok(new { Message = "Login successful" });
                }
                if (result.RequiresTwoFactor)
                {
                    return BadRequest(new { Message = "Requires two-factor authentication" });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return BadRequest(new { Message = "User account locked out" });
                }
                else
                {
                    return BadRequest(new { Message = "Invalid login attempt" });
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }


    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.IdentityModel.Tokens;
//using Client.Areas.Identity.Data;
//using System.ComponentModel.DataAnnotations;

//namespace Client.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserManager<ClientUser> _userManager;
//        private readonly SignInManager<ClientUser> _signInManager;
//        private readonly ILogger<AuthController> _logger;
//        private readonly IConfiguration _configuration;

//        public AuthController(UserManager<ClientUser> userManager, SignInManager<ClientUser> signInManager, ILogger<AuthController> logger, IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _logger = logger;
//            _configuration = configuration;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new ClientUser { UserName = model.Email, Email = model.Email };
//                var result = await _userManager.CreateAsync(user, model.Password);

//                if (result.Succeeded)
//                {
//                    _logger.LogInformation("User created a new account with password.");

//                    // Optionally sign in the user immediately after registration
//                    await _signInManager.SignInAsync(user, isPersistent: false);

//                    var token = GenerateJwtToken(model.Email);
//                    return Ok(new { Token = token });
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }

//                return BadRequest(ModelState);
//            }

//            return BadRequest(ModelState);
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

//                if (result.Succeeded)
//                {
//                    _logger.LogInformation("User logged in.");
//                    var token = GenerateJwtToken(model.Email);
//                    return Ok(new { Token = token });
//                }
//                if (result.RequiresTwoFactor)
//                {
//                    return BadRequest(new { Message = "Requires two-factor authentication" });
//                }
//                if (result.IsLockedOut)
//                {
//                    _logger.LogWarning("User account locked out.");
//                    return BadRequest(new { Message = "User account locked out" });
//                }
//                else
//                {
//                    return BadRequest(new { Message = "Invalid login attempt" });
//                }
//            }

//            return BadRequest(ModelState);
//        }

//        private string GenerateJwtToken(string email)
//        {
//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, email),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"])),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }

//    public class RegisterModel
//    {
//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
//        public string ConfirmPassword { get; set; }
//    }

//    public class LoginModel
//    {
//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }

//        public bool RememberMe { get; set; }
//    }
//}

////using Microsoft.AspNetCore.Authorization;
////using Microsoft.AspNetCore.Mvc;

////namespace Client.Controllers
////{
////    public class AuthController : Controller
////    {
////        public IActionResult Index()
////        {
////            return View();
////        }

////        public IActionResult Register()
////        {
////            return View();
////        }
////        public IActionResult Login()
////        {
////            return View();
////        }

////        public IActionResult GetUsers()
////        {
////            return View();
////        }

////    }
////}
