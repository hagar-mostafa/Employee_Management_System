using Grad_Project.Data;
using Grad_Project.Models;
using Grad_Project.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(EmployeeParams employeeParams)
        {
            var spec = new EmployeeSpecification(employeeParams);
            IQueryable<Employee> query = _context.Employees;
            var finalQuery = SpecificationEvaluator.CreateQuery(query, spec);

            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.JobTitles = await _context.JobTitles.ToListAsync();
            var employees = await finalQuery.ToListAsync();
           
            return View(employees);
        }



        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.JobTitles = await _context.JobTitles.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _context.Departments.ToListAsync();
                ViewBag.JobTitles = await _context.JobTitles.ToListAsync();


                return View(employee);
            }

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.JobTitles = await _context.JobTitles.ToListAsync();


            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _context.Departments.ToListAsync();
                ViewBag.JobTitles = await _context.JobTitles.ToListAsync();


                return View(employee);
            }

            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Employees.AnyAsync(e => e.Id == employee.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.JobTitle)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
