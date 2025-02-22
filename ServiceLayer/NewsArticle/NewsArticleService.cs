using AutoMapper;
using RepositoryLayer.Enums;
using RepositoryLayer.NewsArticles;
using ServiceLayer.Models;

namespace ServiceLayer.NewsArticle
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public NewsArticleService(INewsArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetActiveNewsArticlesAsync()
        {
            var articles = await _articleRepository.ListAllAsync();
            articles = articles.Where(a => a.NewsStatus == NewsStatus.Active);
            var articleDtos = _mapper.Map<IEnumerable<NewsArticleDTO>>(articles);
            return articleDtos;
        }

        public async Task<NewsArticleDTO?> GetNewsArticleByIdAsync(string articleId)
        {
            var article = await _articleRepository.GetArticleByIdAsync(articleId);
            if (article == null || article.NewsStatus != NewsStatus.Active)
            {
                return null;
            }
            return _mapper.Map<NewsArticleDTO>(article);
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetActiveTopRecentNewsArticlesAysnc(
            int count
        )
        {
            var articles = await _articleRepository.ListAllAsync();
            articles = articles
                .Where(a => a.NewsStatus == NewsStatus.Active)
                .Take(count)
                .OrderBy(a => a.ModifiedDate);
            return _mapper.Map<IEnumerable<NewsArticleDTO>>(articles);
        }
    }
}
