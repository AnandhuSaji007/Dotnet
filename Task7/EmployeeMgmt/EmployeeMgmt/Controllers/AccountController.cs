using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMgmt.Controllers
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string SelectedRole { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
        public List<string> Roles { get; set; } = new() { "Admin", "Manager", "Employee" };
    }

    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(model.SelectedRole))
            {
                await _signInManager.SignOutAsync();
                ModelState.AddModelError("", "Selected role does not match your account role.");
                return View(model);
            }

            return RedirectToAction("Index", "Employee");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
