namespace ZD.WikiFuzz.Domain.ArticleIndex;

public class ArticleIndex
{
    public long Id { get; init; }

    public long BytesToSeek { get; init; }
    public long ArticleId { get; init; }
    public string ArticleTitle { get; init; } = null!;
}