using ServiceLayer.Models;

namespace ServiceLayer.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int categoryId);

        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto);

        Task<int?> UpdateCategoryAsync(CategoryDTO categoryDto);
        Task<int?> DeleteCategoryAsync(int categoryId);
    }
}
