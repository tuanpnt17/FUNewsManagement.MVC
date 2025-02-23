using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FUNewsDBContext _context;

        public CategoryRepository(FUNewsDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> ListAllAsync()
        {
            var categories = await _context
                .Categories.Include(c => c.NewsArticles)
                .Include(c => c.ParentCategory)
                .ToListAsync();
            return categories;
        }

        public Task<Category> CreateAccountAsync(Category t)
        {
            throw new NotImplementedException();
        }

        public Task<int?> UpdateAccountAsync(Category t)
        {
            throw new NotImplementedException();
        }

        public Task<int?> DeleteAccountAsync(Category? t)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
