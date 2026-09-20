using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grad_Project.Models;
using Grad_Project.Data;

namespace Grad_Project.Pages.JobTitlePages;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
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

        var jobtitle = await _context.JobTitles
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (jobtitle is null)
        {
            return NotFound();
        }

        JobTitle = jobtitle;
        return Page();
    }
}