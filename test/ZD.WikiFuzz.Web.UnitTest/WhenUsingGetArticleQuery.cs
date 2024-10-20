using System.Collections.Concurrent;
using FluentAssertions;
using NSubstitute;
using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Application.Index.Query;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Web.UnitTest;

public class WhenUsingGetArticleQuery
{
    private readonly IGetArticleQuery _getArticleQuery;

    public static TheoryData<string, string[]> GetArticleNamesTestData =>
        new()
        {
            {
                "Art",
                ["Article A", "Article B", "Article C", "Article D", "Article E", "Article F", "Article G", "Article H"]
            },
            { "Colu", ["Column A", "Column B"] }
        };

    public WhenUsingGetArticleQuery()
    {
        var generateIndexCommand = Substitute.For<IGenerateIndexCommand>();

        ConcurrentDictionary<string, ArticleIndex> articleIndexDictionary =
            new ConcurrentDictionary<string, ArticleIndex>();

        articleIndexDictionary.TryAdd("Article A",
            new ArticleIndex { BytesToSeek = 128, ArticleId = 256, ArticleName = "Article A" });
        articleIndexDictionary.TryAdd("Article B",
            new ArticleIndex { BytesToSeek = 512, ArticleId = 1024, ArticleName = "Article B" });
        articleIndexDictionary.TryAdd("Article C",
            new ArticleIndex { BytesToSeek = 2048, ArticleId = 4096, ArticleName = "Article C" });
        articleIndexDictionary.TryAdd("Article D",
            new ArticleIndex { BytesToSeek = 8192, ArticleId = 16384, ArticleName = "Article D" });
        articleIndexDictionary.TryAdd("Article E",
            new ArticleIndex { BytesToSeek = 32768, ArticleId = 65536, ArticleName = "Article E" });
        articleIndexDictionary.TryAdd("Article F",
            new ArticleIndex { BytesToSeek = 131072, ArticleId = 262144, ArticleName = "Article F" });
        articleIndexDictionary.TryAdd("Article G",
            new ArticleIndex { BytesToSeek = 524288, ArticleId = 1048576, ArticleName = "Article G" });
        articleIndexDictionary.TryAdd("Article H",
            new ArticleIndex { BytesToSeek = 2097152, ArticleId = 4194304, ArticleName = "Article H" });
        articleIndexDictionary.TryAdd("Column A",
            new ArticleIndex { BytesToSeek = 8388608, ArticleId = 16777216, ArticleName = "Column A" });
        articleIndexDictionary.TryAdd("Column B",
            new ArticleIndex { BytesToSeek = 33554432, ArticleId = 67108864, ArticleName = "Column B" });

        _getArticleQuery = new GetArticleQuery(generateIndexCommand)
        { ArticleIndexDictionary = articleIndexDictionary };
    }

    [Theory]
    [InlineData("Article A", 128, 256)]
    [InlineData("Article B", 512, 1024)]
    [InlineData("Article C", 2048, 4096)]
    [InlineData("Article D", 8192, 16384)]
    [InlineData("Article E", 32768, 65536)]
    [InlineData("Article F", 131072, 262144)]
    [InlineData("Article G", 524288, 1048576)]
    [InlineData("Article H", 2097152, 4194304)]
    [InlineData("Column A", 8388608, 16777216)]
    [InlineData("Column B", 33554432, 67108864)]
    public void Then_GetArticleIndex_Should_ReturnCorrectResult(string articleName, long bytesToSeek, long articleId)
    {
        ArticleIndex? articleIndex = _getArticleQuery.GetArticleIndex(articleName);

        articleIndex.Should().NotBeNull();
        articleIndex!.BytesToSeek.Should().Be(bytesToSeek);
        articleIndex.ArticleId.Should().Be(articleId);
        articleIndex.ArticleName.Should().Be(articleName);
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

    [Theory]
    [MemberData(nameof(GetArticleNamesTestData))]
    public void Then_GetArticleNames_Should_ReturnCorrectResult(string partialArticleName,
        string[] expectedReturnedArticleNames)
    {
        IEnumerable<string> articleNames = _getArticleQuery.GetArticleNames(partialArticleName);

        articleNames.Should().BeEquivalentTo(expectedReturnedArticleNames);
    }

    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    [InlineData("A")]
    public void Then_GetArticleNames_Should_ReturnEmptyList(string? partialArticleName)
    {
        IEnumerable<string> articleNames = _getArticleQuery.GetArticleNames(partialArticleName);

        articleNames.Should().BeEmpty();
    }
}