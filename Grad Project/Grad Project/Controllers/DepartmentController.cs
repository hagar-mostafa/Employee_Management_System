using Grad_Project.Data;
using Grad_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .AsNoTracking()
                .ToListAsync();
            return View(departments);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                .Include(d => d.Employees)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null) return NotFound();

            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Department department)
        {
            if (!ModelState.IsValid) return View(department);

            _context.Add(department);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Department created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id) return NotFound();

            if (!ModelState.IsValid) return View(department);

            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Departments.AnyAsync(d => d.Id == id))
                    return NotFound();
                throw;
            }

            TempData["Success"] = "Department updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                .Include(d => d.Employees)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null) return NotFound();

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null) return NotFound();

            // منع حذف قسم فيه موظفين
            if (department.Employees != null && department.Employees.Count > 0)
            {
                ModelState.AddModelError(string.Empty,
                    "Cannot delete this department because it has employees. Move or delete them first.");
                return View(department);
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Department deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
