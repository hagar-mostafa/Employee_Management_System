using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grad_Project.Models;
using Grad_Project.Data;

namespace Grad_Project.Pages.JobTitlePages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<JobTitle> JobTitles { get; set; } = new List<JobTitle>();

    public async Task OnGetAsync()
    {
        JobTitles = await _context.JobTitles
            .AsNoTracking()
            .OrderBy(j => j.Title)
            .ToListAsync();
    }
}