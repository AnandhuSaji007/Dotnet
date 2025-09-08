using Microsoft.AspNetCore.Mvc;

namespace MiniEmployeeManagement.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Select()
        {
            return View();
        }
    }
}
