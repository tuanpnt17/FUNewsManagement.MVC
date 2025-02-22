using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Category;

namespace PhamNguyenTrongTuanMVC.Views.Shared.Components.Category
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return View(categories);
        }
    }
}
