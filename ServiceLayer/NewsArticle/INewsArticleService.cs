using ServiceLayer.Models;

namespace ServiceLayer.NewsArticle
{
    public interface INewsArticleService
    {
        Task<IEnumerable<NewsArticleDTO>> GetActiveNewsArticlesAsync();
        Task<NewsArticleDTO?> GetNewsArticleByIdAsync(string articleId);

        Task<IEnumerable<NewsArticleDTO>> GetActiveTopRecentNewsArticlesAysnc(int count);
    }
}
