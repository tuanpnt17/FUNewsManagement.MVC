using ServiceLayer.Models;

namespace ServiceLayer.NewsArticle
{
    public interface INewsArticleService
    {
        Task<IEnumerable<NewsArticleDTO>> GetActiveNewsArticlesAsync();
        Task<IEnumerable<NewsArticleDTO>> GetAllNewsArticleAsync();
        Task<NewsArticleDTO?> GetNewsArticleByIdAsync(string articleId);

        Task<IEnumerable<NewsArticleDTO>> GetActiveTopRecentNewsArticlesAysnc(int count);
    }
}
