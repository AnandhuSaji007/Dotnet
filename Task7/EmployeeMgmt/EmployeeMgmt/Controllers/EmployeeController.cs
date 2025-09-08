using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EmployeeMgmt.Data;
using EmployeeMgmt.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeMgmt.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index() => View(await _context.Employees.ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var emp = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [AllowAnonymous]
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid) return View(employee);
            employee.CreatedByUserId = _userManager.GetUserId(User);
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id) return NotFound();
            if (!ModelState.IsValid) return View(employee);

            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var emp = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id) => _context.Employees.Any(e => e.Id == id);
    }
}
