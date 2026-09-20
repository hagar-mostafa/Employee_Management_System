using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grad_Project.Models;
using Grad_Project.Data;

namespace Grad_Project.Pages.JobTitlePages;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var jobTitleToUpdate = await _context.JobTitles.FindAsync(JobTitle.Id);
        if (jobTitleToUpdate is null)
        {
            return NotFound();
        }

        var title = JobTitle.Title.Trim();

        if (await _context.JobTitles.AnyAsync(j => j.Title == title && j.Id != JobTitle.Id))
        {
            ModelState.AddModelError("JobTitle.Title", "This job title already exists.");
            return Page();
        }

        // Only Title is updated (prevents overposting)
        jobTitleToUpdate.Title = title;
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}