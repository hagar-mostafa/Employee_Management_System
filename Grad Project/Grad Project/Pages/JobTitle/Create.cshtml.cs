using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grad_Project.Models;
using Grad_Project.Data;

namespace Grad_Project.Pages.JobTitlePages;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Grad_Project.Models.JobTitle JobTitle { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var title = JobTitle.Title.Trim();

        if (await _context.JobTitles.AnyAsync(j => j.Title == title))
        {
            ModelState.AddModelError("JobTitle.Title", "This job title already exists.");
            return Page();
        }
        // Only Title is taken from the form (prevents overposting)
        _context.JobTitles.Add(new JobTitle { Title = title });
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}