using AutoMapper;
using RepositoryLayer.Entities;
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

        public async Task<IEnumerable<NewsArticleDTO>> GetAllNewsArticleAsync()
        {
            var articles = await _articleRepository.ListAllAsync();
            var articleDtos = _mapper.Map<IEnumerable<NewsArticleDTO>>(articles);
            return articleDtos;
        }

        public async Task<NewsArticleDTO?> GetActiveNewsArticleByIdAsync(string articleId)
        {
            var article = await _articleRepository.GetArticleByIdAsync(articleId);
            if (article == null || article.NewsStatus != NewsStatus.Active)
            {
                return null;
            }
            return _mapper.Map<NewsArticleDTO>(article);
        }

        public async Task<NewsArticleDTO?> GetNewsArticleByIdAsync(string articleId)
        {
            var article = await _articleRepository.GetArticleByIdAsync(articleId);
            if (article == null)
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
                .OrderByDescending(a => a.ModifiedDate);
            return _mapper.Map<IEnumerable<NewsArticleDTO>>(articles);
        }

        public async Task<NewsArticleDTO> CreateNewsArticleAsync(
            NewsArticleDTO articleDto,
            string currentUserId
        )
        {
            if (int.TryParse(currentUserId, out var userId))
            {
                articleDto.CreatedById = userId;
                articleDto.UpdatedById = userId;
            }
            articleDto.CreatedDate = DateTime.UtcNow;
            articleDto.ModifiedDate = DateTime.UtcNow;
            var article = _mapper.Map<RepositoryLayer.Entities.NewsArticle>(articleDto);
            if (articleDto.TagIds.Any())
            {
                article.NewsTags = articleDto
                    .TagIds.Select(tagId => new NewsTag
                    {
                        TagId = tagId,
                        NewsArticleId = article.NewsArticleId,
                    })
                    .ToList();
            }
            var addedNewsArticle = await _articleRepository.CreateAsync(article);
            var articleDtoToReturn = _mapper.Map<NewsArticleDTO>(addedNewsArticle);
            return articleDtoToReturn;
        }

        public async Task<int?> UpdateNewsArticleAsync(
            NewsArticleDTO articleDto,
            string currentUserId
        )
        {
            if (int.TryParse(currentUserId, out var userId))
            {
                articleDto.UpdatedById = userId;
            }
            articleDto.ModifiedDate = DateTime.UtcNow;
            var article = _mapper.Map<RepositoryLayer.Entities.NewsArticle>(articleDto);
            if (articleDto.TagIds.Any())
            {
                article.NewsTags = articleDto
                    .TagIds.Select(tagId => new NewsTag
                    {
                        TagId = tagId,
                        NewsArticleId = article.NewsArticleId,
                    })
                    .ToList();
            }
            var effectedRow = await _articleRepository.UpdateAsync(article);
            return effectedRow;
        }

        public async Task<int?> DeleteNewsArticleAsync(string articleId)
        {
            var article = await _articleRepository.GetArticleByIdAsync(articleId);
            if (article == null)
                return 0;
            article.NewsStatus = NewsStatus.Inactive;
            article.ModifiedDate = DateTime.Now;
            var effectedRow = await _articleRepository.UpdateAsync(article);
            return effectedRow;
        }
    }
}
