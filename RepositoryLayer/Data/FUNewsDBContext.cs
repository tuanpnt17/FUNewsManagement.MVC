using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.Entities;
using RepositoryLayer.Enums;

namespace RepositoryLayer.Data
{
    public class FUNewsDBContext : DbContext
    {
        public FUNewsDBContext(DbContextOptions<FUNewsDBContext> options)
            : base(options) { }

        public DbSet<SystemAccount> SystemAccounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nếu bạn muốn dùng Composite Key cho NewsTag:
            modelBuilder.Entity<NewsTag>().HasKey(nt => new { nt.NewsArticleId, nt.TagId });

            // Nếu có self-referencing Category
            modelBuilder
                .Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(pc => pc.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1-n: 1 Category -> nhiều NewsArticle
            modelBuilder
                .Entity<NewsArticle>()
                .HasOne(na => na.Category)
                .WithMany(c => c.NewsArticles)
                .HasForeignKey(na => na.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ví dụ cấu hình 1-n: 1 Account có nhiều CreatedArticles
            modelBuilder
                .Entity<NewsArticle>()
                .HasOne(na => na.CreatedBy)
                .WithMany(a => a.CreatedArticles)
                .HasForeignKey(na => na.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);

            // Ví dụ cấu hình 1-n: 1 Account có nhiều UpdatedArticles
            modelBuilder
                .Entity<NewsArticle>()
                .HasOne(na => na.UpdatedBy)
                .WithMany(a => a.UpdatedArticles)
                .HasForeignKey(na => na.UpdatedById)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ NewsTag - Tag
            modelBuilder
                .Entity<NewsTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NewsTags)
                .HasForeignKey(nt => nt.TagId);

            // Converter cho NewsStatus: lưu dưới dạng bit (bool) trong DB
            var newsStatusConverter = new ValueConverter<NewsStatus, bool>(
                v => v == NewsStatus.Active,
                v => v ? NewsStatus.Active : NewsStatus.Inactive
            );

            modelBuilder
                .Entity<NewsArticle>()
                .Property(na => na.NewsStatus)
                .HasConversion(newsStatusConverter)
                .HasColumnType("bit");

            // Converter cho CategoryStatus: lưu dưới dạng bit trong DB
            var categoryStatusConverter = new ValueConverter<CategoryStatus, bool>(
                v => v == CategoryStatus.Active,
                v => v ? CategoryStatus.Active : CategoryStatus.Inactive
            );

            // Vì cột trong DB tên là IsActive, ta cấu hình như sau:
            modelBuilder
                .Entity<Category>()
                .Property(typeof(CategoryStatus), "CategoryStatus")
                .HasConversion(categoryStatusConverter)
                .HasColumnName("IsActive")
                .HasColumnType("bit");
        }
    }
}
