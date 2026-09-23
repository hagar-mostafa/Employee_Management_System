using Grad_Project.Data;
using Grad_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            var viewModel = new DashboardViewModel
            {
                TotalEmployees = await _context.Employees.CountAsync(),
                TotalDepartments = await _context.Departments.CountAsync(),
                TotalJobTitles = await _context.JobTitles.CountAsync(),

                // Count employees hired in the current month
                HiredThisMonth = await _context.Employees
                                      .Where(e => e.HireDate >= firstDayOfMonth)
                                      .CountAsync(),

                // Get the last 5 employees added to the database
                RecentEmployees = await _context.Employees
                                        .Include(e => e.Department)
                                        .Include(e => e.JobTitle)
                                        .OrderByDescending(e => e.Id)
                                        .Take(5)
                                        .ToListAsync()
            };

            return View(viewModel);
        }
    }
}
