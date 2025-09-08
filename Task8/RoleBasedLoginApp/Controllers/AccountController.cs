using Microsoft.AspNetCore.Mvc;
using RoleBasedLoginApp.Models;
using RoleBasedLoginApp.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RoleBasedLoginApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService) => _userService = userService;

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userService.ValidateUserAsync(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return user.Role switch
            {
                "Admin" => RedirectToAction("Admin", "Home"),
                "Manager" => RedirectToAction("Manager", "Home"),
                "Employee" => RedirectToAction("Employee", "Home"),
                _ => RedirectToAction("Login", "Account")
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
