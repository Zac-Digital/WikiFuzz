using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using ZD.WikiFuzz.Application.Index.Query;
using ZD.WikiFuzz.Web.Pages;

namespace ZD.WikiFuzz.Web.UnitTest;

public class WhenUsingIndex
{
    private readonly IndexModel _indexModel;

    public WhenUsingIndex()
    {
        IGetArticleQuery getArticleQuery = Substitute.For<IGetArticleQuery>();
        _indexModel = new IndexModel(getArticleQuery);
    }

    [Fact]
    public void Then_OnGet_Should_Return_OK()
    {
        IActionResult result = _indexModel.OnGet();

        result.Should().BeOfType<PageResult>();
    }

    [Fact]
    public void Then_OnPost_Should_Return_OK()
    {
        IActionResult result = _indexModel.OnPost();

        result.Should().BeOfType<PageResult>();
    }
}