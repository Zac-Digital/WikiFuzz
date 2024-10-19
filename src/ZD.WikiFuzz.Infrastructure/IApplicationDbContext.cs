using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Infrastructure;

public interface IApplicationDbContext
{
    public IQueryable<ArticleIndex> ArticleIndices { get; }

    public void AddArticleIndex(ArticleIndex index);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}