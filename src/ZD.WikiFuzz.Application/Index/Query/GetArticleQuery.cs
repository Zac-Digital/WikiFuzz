using System.Collections.Concurrent;
using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Application.Index.Query;

public class GetArticleQuery : IGetArticleQuery
{
    private readonly ConcurrentDictionary<string, ArticleIndex> _articleIndexDictionary;

    public GetArticleQuery(IGenerateIndexCommand generateIndexCommand)
    {
        _articleIndexDictionary = new ConcurrentDictionary<string, ArticleIndex>();
        Task.Run(() =>
            generateIndexCommand.Generate(
                new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Wiki-Index.txt")),
                _articleIndexDictionary));
    }
}