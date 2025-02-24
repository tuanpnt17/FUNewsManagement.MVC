using ServiceLayer.Models;

namespace ServiceLayer.NewsArticle
{
    public interface INewsArticleService
    {
        Task<IEnumerable<NewsArticleDTO>> GetActiveNewsArticlesAsync();
        Task<IEnumerable<NewsArticleDTO>> GetAllNewsArticleAsync();
        Task<NewsArticleDTO?> GetActiveNewsArticleByIdAsync(string articleId);
        Task<NewsArticleDTO?> GetNewsArticleByIdAsync(string articleId);

        Task<IEnumerable<NewsArticleDTO>> GetActiveTopRecentNewsArticlesAysnc(int count);

        Task<NewsArticleDTO> CreateNewsArticleAsync(
            NewsArticleDTO newsArticleDto,
            string currentUserId
        );

        Task<int?> UpdateNewsArticleAsync(NewsArticleDTO newsArticleDto, string currentUserId);
        Task<int?> DeleteNewsArticleAsync(string newsArticleId);
    }
}
