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

    private ConcurrentDictionary<string, ArticleIndex> _articleIndexDictionary = null!;

    public WhenUsingGetArticleQuery()
    {
        var generateIndexCommand = Substitute.For<IGenerateIndexCommand>();

        generateIndexCommand
            .When(x => x.Generate(Arg.Any<StreamReader>(), Arg.Any<ConcurrentDictionary<string, ArticleIndex>>()))
            .Do(_ => { _articleIndexDictionary = new ConcurrentDictionary<string, ArticleIndex>(); });

        _getArticleQuery = new GetArticleQuery(generateIndexCommand);
    }

    // TODO: Temporary
    [Fact]
    public void ExampleTest()
    {
        const int x = 4;
        const int y = 8;

        const int z = x * y;

        z.Should().Be(32);
    }
}