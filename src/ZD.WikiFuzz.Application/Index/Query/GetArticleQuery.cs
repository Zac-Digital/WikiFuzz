using System.Collections.Concurrent;
using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Application.Index.Query;

public class GetArticleQuery : IGetArticleQuery
{
    private readonly ConcurrentDictionary<string, ArticleIndex> _articleIndexDictionary;

    public GetArticleQuery()
    {
        _articleIndexDictionary = new ConcurrentDictionary<string, ArticleIndex>();
        Task.Run(() => new GenerateIndexCommand().Generate(_articleIndexDictionary));
    }
}