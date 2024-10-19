using Microsoft.EntityFrameworkCore;
using ZD.WikiFuzz.Domain.ArticleIndex;

namespace ZD.WikiFuzz.Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ArticleIndex> ArticleIndices { get; init; } = null!;
    IQueryable<ArticleIndex> IApplicationDbContext.ArticleIndices => ArticleIndices;

    public void AddArticleIndex(ArticleIndex index) => ArticleIndices.Add(index);

    public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticleIndex>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.BytesToSeek).IsRequired();
            entity.Property(e => e.ArticleId).IsRequired();
            entity.Property(e => e.ArticleTitle).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}