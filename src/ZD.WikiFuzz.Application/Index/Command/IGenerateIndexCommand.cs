using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Application.Index.Command;

public interface IGenerateIndexCommand
{
    public Dictionary<string, ArticleIndex> GenerateIndices();
}