using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.NewsArticles
{
    public interface INewsArticleRepository : IGenericRepository<NewsArticle>
    {
        Task<NewsArticle?> GetArticleByIdAsync(string id);
    }
}
