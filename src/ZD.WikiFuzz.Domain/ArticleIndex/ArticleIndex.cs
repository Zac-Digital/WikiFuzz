namespace ZD.WikiFuzz.Domain.ArticleIndex;

public class ArticleIndex
{
    public long BytesToSeek { get; init; }
    public long ArticleId { get; init; }
    public string ArticleName { get; init; } = null!;
}