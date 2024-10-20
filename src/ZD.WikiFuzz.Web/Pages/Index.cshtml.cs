using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZD.WikiFuzz.Application.Index.Query;

namespace ZD.WikiFuzz.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IGetArticleQuery _getArticleQuery;

    [BindProperty(SupportsGet = true)] public string? SearchText { get; set; }
    public IEnumerable<string> SearchResults { get; set; } = [];

    public IndexModel(IGetArticleQuery getArticleQuery)
    {
        _getArticleQuery = getArticleQuery;
    }

    public IActionResult OnGet()
    {
        SearchResults = _getArticleQuery.GetArticleNames(SearchText);

        if (Request.Headers.XRequestedWith.Equals("XMLHttpRequest"))
        {
            return new JsonResult(SearchResults);
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}