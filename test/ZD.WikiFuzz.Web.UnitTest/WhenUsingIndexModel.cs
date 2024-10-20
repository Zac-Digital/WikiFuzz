using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using ZD.WikiFuzz.Application.Index.Query;
using ZD.WikiFuzz.Web.Pages;

namespace ZD.WikiFuzz.Web.UnitTest;

public class WhenUsingIndexModel
{
    private readonly IndexModel _indexModel;

    private readonly HttpContext _httpContext;

    public WhenUsingIndexModel()
    {
        _httpContext = new DefaultHttpContext();
        ModelStateDictionary modelState = new ModelStateDictionary();
        ActionContext actionContext = new ActionContext(_httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        EmptyModelMetadataProvider modelMetadataProvider = new EmptyModelMetadataProvider();
        ViewDataDictionary viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        PageContext pageContext = new PageContext(actionContext) { ViewData = viewData };

        _indexModel = new IndexModel(Substitute.For<IGetArticleQuery>()) { PageContext = pageContext };
    }

    [Fact]
    public void Then_OnGet_Should_Return_OK()
    {
        IActionResult result = _indexModel.OnGet();

        result.Should().BeOfType<PageResult>();
    }

    [Fact]
    public void Then_OnGet_Should_Return_OK_With_Header()
    {
        _httpContext.Request.Headers.XRequestedWith = "XMLHttpRequest";

        IActionResult result = _indexModel.OnGet();

        result.Should().BeOfType<JsonResult>();
    }

    [Fact]
    public void Then_OnPost_Should_Return_OK()
    {
        IActionResult result = _indexModel.OnPost();

        result.Should().BeOfType<PageResult>();
    }
}