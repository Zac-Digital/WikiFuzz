using System.Collections.Concurrent;
using System.Text;
using FluentAssertions;
using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Web.UnitTest;

public class WhenUsingGenerateIndexCommand
{
    private readonly StreamReader _streamReader;

    private readonly ConcurrentDictionary<string, ArticleIndex> _expectedArticleIndexDictionary;

    public WhenUsingGenerateIndexCommand()
    {
        MemoryStream memoryStream = new MemoryStream();

        using (StreamWriter streamWriter = new StreamWriter(memoryStream, leaveOpen: true))
        {
            streamWriter.WriteLine("0:0:ArticleOne");
            streamWriter.WriteLine("128:64:ArticleTwo");
            streamWriter.WriteLine("131072:16384:ArticleThree");
            streamWriter.WriteLine("16:262144:ArticleFour");
            streamWriter.WriteLine("32:8:Article:Five");
            streamWriter.WriteLine("8192:1:Article:S:i:x");

            streamWriter.Flush();
        }

        memoryStream.Position = 0;

        _streamReader = new StreamReader(memoryStream, Encoding.UTF8);

        _expectedArticleIndexDictionary = new ConcurrentDictionary<string, ArticleIndex>();

        _expectedArticleIndexDictionary.TryAdd("ArticleOne", new ArticleIndex
        {
            BytesToSeek = 0,
            ArticleId = 0
        });
        _expectedArticleIndexDictionary.TryAdd("ArticleTwo", new ArticleIndex
        {
            BytesToSeek = 128,
            ArticleId = 64
        });
        _expectedArticleIndexDictionary.TryAdd("ArticleThree", new ArticleIndex
        {
            BytesToSeek = 131072,
            ArticleId = 16384
        });
        _expectedArticleIndexDictionary.TryAdd("ArticleFour", new ArticleIndex
        {
            BytesToSeek = 16,
            ArticleId = 262144
        });
        _expectedArticleIndexDictionary.TryAdd("Article:Five", new ArticleIndex
        {
            BytesToSeek = 32,
            ArticleId = 8
        });
        _expectedArticleIndexDictionary.TryAdd("Article:S:i:x", new ArticleIndex
        {
            BytesToSeek = 8192,
            ArticleId = 1
        });
    }

    [Fact]
    public void Then_Generate_ShouldPopulateDictionary()
    {
        IGenerateIndexCommand generateIndexCommand = new GenerateIndexCommand();

        ConcurrentDictionary<string, ArticleIndex>
            articleIndexDictionary = new ConcurrentDictionary<string, ArticleIndex>();

        generateIndexCommand.Generate(_streamReader, articleIndexDictionary);

        articleIndexDictionary.Should().NotBeEmpty();

        articleIndexDictionary.Keys.Should().BeEquivalentTo(_expectedArticleIndexDictionary.Keys);

        foreach (string articleString in articleIndexDictionary.Keys)
        {
            ArticleIndex articleIndex = articleIndexDictionary[articleString];
            ArticleIndex expectedArticleIndex = _expectedArticleIndexDictionary[articleString];

            articleIndex.Should().BeEquivalentTo(expectedArticleIndex);
        }
    }
}