using Microsoft.AspNetCore.Mvc;
using ServiceLayer.NewsArticle;

namespace PhamNguyenTrongTuanMVC.Views.Shared.Components.RecentNews
{
    public class RecentNewsViewComponent : ViewComponent
    {
        private readonly INewsArticleService _newsArticleService;

        public RecentNewsViewComponent(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _newsArticleService.GetActiveTopRecentNewsArticlesAysnc(5);
            return View(articles);
        }
    }
}
