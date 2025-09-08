using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedLoginApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Admin() => View();

        [Authorize(Roles = "Manager")]
        public IActionResult Manager() => View();

        [Authorize(Roles = "Employee")]
        public IActionResult Employee() => View();
    }
}
