using System.Collections.Concurrent;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Application.Index.Command;

public class GenerateIndexCommand : IGenerateIndexCommand
{
    private readonly int[] _sepIndices = new int[2];

    private void SetSeparatorIndices(string currentLine)
    {
        int sepIndex = 0;
        for (int i = 0; i < currentLine.Length; i++)
        {
            if (currentLine[i] != ':') continue;
            _sepIndices[sepIndex++] = i;
            if (sepIndex == _sepIndices.Length) break;
        }
    }

    private string GetArticleTitle(ref string       currentLine) => new(currentLine.AsSpan()[(_sepIndices[1] + 1)..]);

    private ArticleIndex CreateArticleIndex(ref string currentLine)
    {
        return new ArticleIndex
        {
            BytesToSeek = long.Parse(currentLine.AsSpan()[.._sepIndices[0]]),
            ArticleId = long.Parse(currentLine.AsSpan().Slice(_sepIndices[0] + 1, _sepIndices[1] - _sepIndices[0] - 1))
        };
    }

    public void Generate(StreamReader fileReader, ConcurrentDictionary<string, ArticleIndex> articleIndexDictionary)
    {
        string? currentLine = fileReader.ReadLine();

        while (currentLine is not null)
        {
            SetSeparatorIndices(currentLine);
            ArticleIndex articleIndex = CreateArticleIndex(ref currentLine);

            articleIndexDictionary.TryAdd(GetArticleTitle(ref currentLine), articleIndex);

            currentLine = fileReader.ReadLine();
        }

        fileReader.Close();
    }
}