using System.Collections.Concurrent;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Application.Index.Command;

public interface IGenerateIndexCommand
{
    public void Generate(StreamReader fileReader, ConcurrentDictionary<string, ArticleIndex> articleIndexDictionary);
}