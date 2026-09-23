using Grad_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.EmployeeCount = await _context.Employees.CountAsync();
            ViewBag.DepartmentCount = await _context.Departments.CountAsync();
            ViewBag.JobTitleCount = await _context.JobTitles.CountAsync();
            ViewBag.RecentEmployees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .OrderByDescending(e => e.HireDate)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}
