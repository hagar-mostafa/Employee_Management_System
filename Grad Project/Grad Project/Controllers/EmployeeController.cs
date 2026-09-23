using Grad_Project.Data;
using Grad_Project.Models;
using Grad_Project.Services;
using Grad_Project.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AttachmentService _attachmentService;

        public EmployeeController(AppDbContext context, AttachmentService attachmentService)
        {
            _context = context;
            _attachmentService = attachmentService;

        }

        public async Task<IActionResult> Index(EmployeeParams employeeParams)
        {
            var spec = new EmployeeSpecification(employeeParams);
            spec.AddPagination(employeeParams.PageSize, employeeParams.PageNumber);

            IQueryable<Employee> query = _context.Employees;

            var finalQuery = SpecificationEvaluator.CreateQuery(query, spec);


            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.JobTitles = await _context.JobTitles.ToListAsync();
            var employees = await finalQuery.ToListAsync();
            #region pagination

            var specCount = new EmployeeSpecification(employeeParams);
            IQueryable<Employee> queryCount = _context.Employees;
            var finalQueryCount = SpecificationEvaluator.CreateQuery(queryCount, specCount);

            var totalItems = await finalQueryCount.CountAsync();
            //Console.WriteLine(totalItems);
            ViewBag.CurrentPage = employeeParams.PageNumber;
            ViewBag.PageSize = employeeParams.PageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)employeeParams.PageSize);

            ViewBag.CurrentParams = employeeParams;

            #endregion
            return View(employees);
        }

        public async Task<IActionResult> Picture(int id, CancellationToken ct)
        {
            var employees = _context.Employees;
            var employee = await employees.FirstOrDefaultAsync(e => e.Id == id, ct);
            if (employee == null || string.IsNullOrWhiteSpace(employee.ProfileImagePath)) return NotFound();

            var result = _attachmentService.GetFile(employee.ProfileImagePath, "Images");
            return result == null ? NotFound() : File(result.Value.stream, result.Value.contentType);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.JobTitles = await _context.JobTitles.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _context.Departments.ToListAsync();
                ViewBag.JobTitles = await _context.JobTitles.ToListAsync();



                return View(employee);
            }
            var emp = new Employee
            {
                FullName = employee.FullName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HireDate = employee.HireDate,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                JobTitleId = employee.JobTitleId
            };
            if (employee.ImageFile != null && employee.ImageFile.Length > 0)
            {
                var imagePath = await _attachmentService.UploadAsync(employee.ImageFile.OpenReadStream(), employee.ImageFile.FileName, "Images");
                emp.ProfileImagePath = imagePath;
            }

            await _context.Employees.AddAsync(emp);
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
                _attachmentService.Delete(employee.ProfileImagePath, "Images");
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
