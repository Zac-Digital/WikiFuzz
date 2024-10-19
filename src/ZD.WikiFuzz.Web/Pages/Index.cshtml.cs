using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZD.WikiFuzz.Web.Pages;

public class IndexModel : PageModel
{
    [BindProperty] public string? SearchText { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}