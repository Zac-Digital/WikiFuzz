using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Application.Index.Query;

public interface IGetArticleQuery
{
    public ArticleIndex? GetArticleIndex(string? articleName);
}