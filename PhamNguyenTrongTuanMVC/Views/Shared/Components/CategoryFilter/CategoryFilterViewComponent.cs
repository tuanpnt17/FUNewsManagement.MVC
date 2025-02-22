using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhamNguyenTrongTuanMVC.Models;
using ServiceLayer.Category;
using ServiceLayer.NewsArticle;

namespace PhamNguyenTrongTuanMVC.Views.Shared.Components.CategoryFilter
{
    public class CategoryFilterViewComponent : ViewComponent
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryFilterViewComponent(
            INewsArticleService newsArticleService,
            ICategoryService categoryService,
            IMapper mapper
        )
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _newsArticleService.GetActiveNewsArticlesAsync();
            var categories = await _categoryService.GetCategoriesAsync();

            var matchedCategories = categories
                .Where(c => articles.Any(a => a.CategoryId == c.CategoryId))
                .ToList();
            var matchedCategoryDtos = _mapper.Map<List<CategoryViewModel>>(matchedCategories);

            return View(matchedCategoryDtos);
        }
    }
}
