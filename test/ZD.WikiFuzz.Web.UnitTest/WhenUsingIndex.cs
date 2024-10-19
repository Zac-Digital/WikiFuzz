// using FluentAssertions;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using ZD.WikiFuzz.Web.Pages;
//
// namespace ZD.WikiFuzz.Web.UnitTest;
//
// public class WhenUsingIndex
// {
//     private readonly IndexModel _indexModel = new();
//
//     [Fact]
//     public void Then_OnGet_Should_Return_OK()
//     {
//         IActionResult result = _indexModel.OnGet();
//
//         result.Should().BeOfType<PageResult>();
//     }
//
//     [Fact]
//     public async Task Then_OnPost_Should_Return_OK()
//     {
//         IActionResult result = await _indexModel.OnPostAsync();
//
//         result.Should().BeOfType<PageResult>();
//     }
// }