using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Categories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryByIdAsync(int id);
    }
}
