using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhamNguyenTrongTuanMVC.Models.NewsArticle;
using ServiceLayer.Category;
using ServiceLayer.Models;
using ServiceLayer.NewsArticle;
using ServiceLayer.Tag;

namespace PhamNguyenTrongTuanMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public NewsArticleController(
            INewsArticleService newsArticleService,
            IMapper mapper,
            ICategoryService categoryService,
            ITagService tagService
        )
        {
            _newsArticleService = newsArticleService;
            _mapper = mapper;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _newsArticleService.GetActiveNewsArticlesAsync();
            return View(articles);
        }

        [HttpGet("{articleId}")]
        public async Task<IActionResult> ArticleDetail(string articleId)
        {
            var article = await _newsArticleService.GetActiveNewsArticleByIdAsync(articleId);
            if (article == null)
            {
                return RedirectToAction("Index");
            }
            return View(article);
        }

        //TODO: Pagination & Searching & filtering

        public async Task<IActionResult> List()
        {
            var articles = await _newsArticleService.GetAllNewsArticleAsync();
            var viewModels = _mapper.Map<IEnumerable<ViewNewsArticleViewModel>>(articles);
            return View(viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Add(
            [FromForm] AddNewsArticleViewModel addNewsArticleViewModel
        )
        {
            if (!ModelState.IsValid)
            {
                addNewsArticleViewModel.Categories = await _categoryService.GetCategoriesAsync();
                addNewsArticleViewModel.Tags = await _tagService.GetAllTagsAsync();
                return PartialView("_AddNewsArticleModal", addNewsArticleViewModel);
            }
            var newsArticleDto = _mapper.Map<NewsArticleDTO>(addNewsArticleViewModel);
            var currentUserId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Sid)!.Value;
            await _newsArticleService.CreateNewsArticleAsync(newsArticleDto, currentUserId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddNewsArticleViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(),
                Tags = await _tagService.GetAllTagsAsync(),
            };
            return PartialView("_AddNewsArticleModal", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var newsArticleDto = await _newsArticleService.GetNewsArticleByIdAsync(id);
            var updateNewsArticleViewModel = _mapper.Map<UpdateNewsArticleViewModel>(
                newsArticleDto
            );
            updateNewsArticleViewModel.Categories = await _categoryService.GetCategoriesAsync();
            updateNewsArticleViewModel.Tags = await _tagService.GetAllTagsAsync();

            return PartialView("_UpdateNewsArticleModal", updateNewsArticleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateNewsArticleViewModel updateNewsArticleViewModel)
        {
            if (!ModelState.IsValid)
            {
                updateNewsArticleViewModel.Categories = await _categoryService.GetCategoriesAsync();
                updateNewsArticleViewModel.Tags = await _tagService.GetAllTagsAsync();
                return PartialView("_UpdateNewsArticleModal", updateNewsArticleViewModel);
            }

            var accountDto = _mapper.Map<NewsArticleDTO>(updateNewsArticleViewModel);
            var currentUserId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Sid)!.Value;
            var updatedNewsArticle = await _newsArticleService.UpdateNewsArticleAsync(
                accountDto,
                currentUserId
            );
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var deleteEffected = await _newsArticleService.DeleteNewsArticleAsync(id);
            if (deleteEffected > 0)
            {
                return RedirectToAction("List");
            }
            return BadRequest();
        }
    }
}
