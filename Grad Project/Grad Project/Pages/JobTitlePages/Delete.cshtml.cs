using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grad_Project.Models;
using Grad_Project.Data;

namespace Grad_Project.Pages.JobTitlePages;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    public JobTitle JobTitle { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var jobtitle = await _context.JobTitles.FirstOrDefaultAsync(m => m.Id == id);
        if (jobtitle is null)
        {
            return NotFound();
        }

        JobTitle = jobtitle;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var jobtitle = await _context.JobTitles.FindAsync(id);
        if (jobtitle is null)
        {
            return NotFound();
        }

        // Block deleting a job title that still has employees
        var hasEmployees = await _context.Entry(jobtitle)
            .Collection(j => j.Employees)
            .Query()
            .AnyAsync();

        if (hasEmployees)
        {
            JobTitle = jobtitle;
            ModelState.AddModelError(string.Empty,
                "This job title can't be deleted because employees are assigned to it.");
            return Page();
        }

        _context.JobTitles.Remove(jobtitle);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}