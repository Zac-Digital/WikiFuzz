using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZD.WikiFuzz.Application.Index.Query;

namespace ZD.WikiFuzz.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IGetArticleQuery _getArticleQuery;

    [BindProperty] public string? SearchText { get; set; }

    public IndexModel(IGetArticleQuery getArticleQuery)
    {
        _getArticleQuery = getArticleQuery;
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