using System.Collections.Concurrent;
using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Application.Index.Query;

public class GetArticleQuery : IGetArticleQuery
{
    public ConcurrentDictionary<string, ArticleIndex> ArticleIndexDictionary { get; init; }

    // Stryker disable all
    public GetArticleQuery(IGenerateIndexCommand generateIndexCommand)
    {
        ArticleIndexDictionary = new ConcurrentDictionary<string, ArticleIndex>();
        Task.Run(() =>
            generateIndexCommand.Generate(
                new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Wiki-Index.txt")),
                ArticleIndexDictionary));
    }
    // Stryker restore all

    public ArticleIndex? GetArticleIndex(string? articleName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(articleName);

        return ArticleIndexDictionary.GetValueOrDefault(articleName);
    }
}