using System.Collections.Concurrent;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Application.Index.Query;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Web.UnitTest;

public class WhenUsingGetArticleQuery
{
    private readonly IGetArticleQuery _getArticleQuery;

    public WhenUsingGetArticleQuery()
    {
        var generateIndexCommand = Substitute.For<IGenerateIndexCommand>();

        ConcurrentDictionary<string, ArticleIndex> articleIndexDictionary = new ConcurrentDictionary<string, ArticleIndex>();

        articleIndexDictionary.TryAdd("Article A", new ArticleIndex { BytesToSeek = 128, ArticleId = 256});
        articleIndexDictionary.TryAdd("Article B", new ArticleIndex { BytesToSeek = 512, ArticleId = 1024});
        articleIndexDictionary.TryAdd("Article C", new ArticleIndex { BytesToSeek = 2048, ArticleId = 4096});
        articleIndexDictionary.TryAdd("Article D", new ArticleIndex { BytesToSeek = 8192, ArticleId = 16384});

        _getArticleQuery = new GetArticleQuery(generateIndexCommand) { ArticleIndexDictionary = articleIndexDictionary};
    }

    [Theory]
    [InlineData("Article A", 128, 256)]
    [InlineData("Article B", 512, 1024)]
    [InlineData("Article C", 2048, 4096)]
    [InlineData("Article D", 8192, 16384)]
    public void Then_GetArticleIndex_Should_ReturnCorrectResult(string articleName, long bytesToSeek, long articleId)
    {
        ArticleIndex? articleIndex = _getArticleQuery.GetArticleIndex(articleName);

        articleIndex.Should().NotBeNull();
        articleIndex!.BytesToSeek.Should().Be(bytesToSeek);
        articleIndex.ArticleId.Should().Be(articleId);
    }

    [Fact]
    public void Then_GetArticleIndex_Should_ReturnNull_WhenArticleNotFound()
    {
        ArticleIndex? articleIndex = _getArticleQuery.GetArticleIndex("Article Does Not Exist");

        articleIndex.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Then_GetArticleIndex_Should_ThrowException_When_InputIsMalformed(string? articleName)
    {
        _getArticleQuery.Invoking(x => x.GetArticleIndex(articleName)).Should().Throw<ArgumentException>();
    }
}