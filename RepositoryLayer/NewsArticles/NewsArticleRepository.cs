﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.NewsArticles
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FUNewsDBContext _context;

        public NewsArticleRepository(FUNewsDBContext context)
        {
            _context = context;
        }

        public async Task<NewsArticle?> GetArticleByIdAsync(string id)
        {
            var newsArticle = await _context
                .NewsArticles.Include(a => a.Category)
                .Include(a => a.CreatedBy)
                .Include(a => a.UpdatedBy)
                .Include(a => a.NewsTags!)
                .ThenInclude(n => n.Tag)
                .FirstOrDefaultAsync(a => a.NewsArticleId == id);
            return newsArticle;
        }

        public async Task<IEnumerable<NewsArticle>> ListAllAsync()
        {
            var newsArticles = await _context
                .NewsArticles.Include(a => a.Category)
                .Include(a => a.CreatedBy)
                .Include(a => a.UpdatedBy)
                .Include(a => a.NewsTags)
                .OrderByDescending(a => a.ModifiedDate)
                .ToListAsync();
            return newsArticles;
        }

        public async Task<NewsArticle> CreateAsync(NewsArticle newsArticle)
        {
            var addedAccount = await _context.NewsArticles.AddAsync(newsArticle);
            await _context.SaveChangesAsync();
            return newsArticle;
        }

        public async Task<int?> UpdateAsync(NewsArticle newsArticle)
        {
            var article = await GetArticleByIdAsync(newsArticle.NewsArticleId);
            if (article == null)
            {
                return null;
            }

            article.ModifiedDate = DateTime.UtcNow;
            article.NewsTags = newsArticle.NewsTags;
            article.NewsTitle = newsArticle.NewsTitle;
            article.Headline = newsArticle.Headline;
            article.NewsSource = newsArticle.NewsSource;
            article.NewsContent = newsArticle.NewsContent;
            article.NewsStatus = newsArticle.NewsStatus;
            article.CategoryId = newsArticle.CategoryId;
            article.UpdatedById = newsArticle.UpdatedById;

            await Task.Run(() => _context.NewsArticles.Update(article));
            var effectedRow = await _context.SaveChangesAsync();
            return effectedRow;
        }

        public async Task<int?> DeleteAsync(NewsArticle? deletedAccount)
        {
            var systemAccount = await GetArticleByIdAsync(deletedAccount.NewsArticleId);
            if (systemAccount == null)
            {
                return null;
            }
            await Task.Run(() => _context.NewsArticles.Remove(systemAccount));
            var effectedRow = await _context.SaveChangesAsync();
            return effectedRow;
        }
    }
}
