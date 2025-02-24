using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhamNguyenTrongTuanMVC.Models.Category;
using ServiceLayer.Category;
using ServiceLayer.Models;

namespace PhamNguyenTrongTuanMVC.Controllers
{
    [Authorize(Roles = "Staff")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categoriesDto = await _categoryService.GetCategoriesAsync();
            var categoriesViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categoriesDto);

            return View(categoriesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            [FromForm]
            [Bind("CategoryName, CategoryDescription, CategoryStatus")]
                AddNewCategoryViewModel addNewCategoryViewModel
        )
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AddCategoryModal", addNewCategoryViewModel);
            }
            var categoryDto = _mapper.Map<CategoryDTO>(addNewCategoryViewModel);
            await _categoryService.CreateCategoryAsync(categoryDto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_AddCategoryModal");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDTO = await _categoryService.GetCategoryByIdAsync(id);
            var updateCategoryViewModel = _mapper.Map<UpdateCategoryViewModel>(categoryDTO);

            return PartialView("_UpdateCategoryModal", updateCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoryViewModel updateCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_UpdateCategoryModal", updateCategoryViewModel);
            }

            var accountDto = _mapper.Map<CategoryDTO>(updateCategoryViewModel);

            var updatedCategory = await _categoryService.UpdateCategoryAsync(accountDto);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteEffected = await _categoryService.DeleteCategoryAsync(id);
            if (deleteEffected > 0)
            {
                return RedirectToAction("List");
            }
            return BadRequest();
        }
    }
}
