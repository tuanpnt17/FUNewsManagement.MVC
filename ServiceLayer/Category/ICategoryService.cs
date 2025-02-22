using ServiceLayer.Models;

namespace ServiceLayer.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    }
}
