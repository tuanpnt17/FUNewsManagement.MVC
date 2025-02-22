using Microsoft.AspNetCore.Mvc;
using ServiceLayer.NewsArticle;

namespace PhamNguyenTrongTuanMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;

        public NewsArticleController(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

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
    }
}
