using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZD.WikiFuzz.Application.Index.Command;

namespace ZD.WikiFuzz.Web.Pages;

public class IndexModel : PageModel
{
    [BindProperty] public string? SearchText { get; set; }

    public IndexModel(IGenerateIndexCommand generateIndexCommand)
    {
        Task.Run(generateIndexCommand.GenerateIndices);
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}