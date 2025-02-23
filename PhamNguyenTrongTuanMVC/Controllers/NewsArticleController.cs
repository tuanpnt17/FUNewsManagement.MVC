using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhamNguyenTrongTuanMVC.Models.NewsArticle;
using ServiceLayer.NewsArticle;

namespace PhamNguyenTrongTuanMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly IMapper _mapper;

        public NewsArticleController(INewsArticleService newsArticleService, IMapper mapper)
        {
            _newsArticleService = newsArticleService;
            _mapper = mapper;
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
            var article = await _newsArticleService.GetNewsArticleByIdAsync(articleId);
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
    }
}
