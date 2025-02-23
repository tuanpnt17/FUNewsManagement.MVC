using AutoMapper;
using RepositoryLayer.Categories;
using ServiceLayer.Models;

namespace ServiceLayer.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.ListAllAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoryDtos;
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return categoryDto;
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<RepositoryLayer.Entities.Category>(categoryDto);
            var addedCategory = await _categoryRepository.CreateAsync(category);
            var categoryDtoToReturn = _mapper.Map<CategoryDTO>(addedCategory);
            return categoryDtoToReturn;
        }

        public async Task<int?> UpdateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<RepositoryLayer.Entities.Category>(categoryDto);
            var effectedRow = await _categoryRepository.UpdateAsync(category);
            return effectedRow;
        }

        public async Task<int?> DeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
                return 0;
            var countNews = category.NewsArticles?.Count();
            if (countNews == null || countNews > 0)
            {
                return 0;
            }
            var effectedRow = await _categoryRepository.DeleteAsync(category);
            return effectedRow;
        }
    }
}
