using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhamNguyenTrongTuanMVC.Models.NewsArticle;
using ServiceLayer.Models;
using ServiceLayer.NewsArticle;

namespace PhamNguyenTrongTuanMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly IMapper _mapper;

        public DashboardController(INewsArticleService newsArticleService, IMapper mapper)
        {
            _newsArticleService = newsArticleService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Report(
            DateTime? startDate,
            DateTime? endDate,
            int? pageNumber
        )
        {
            startDate ??= DateTime.UtcNow.AddDays(-30);
            endDate ??= DateTime.UtcNow;
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            var newsArticles = await _newsArticleService.GetAllNewsArticleAsync();
            if (startDate <= endDate)
            {
                newsArticles = newsArticles.Where(n =>
                    n.ModifiedDate.Date >= startDate.Value.Date
                    && n.ModifiedDate.Date <= endDate.Value.Date
                );
            }
            else
            {
                TempData["DateRangeError"] =
                    "Start date must be earlier than end date. Please adjust the dates.";
            }

            var viewReportNewsArticleViewModel = _mapper.Map<IEnumerable<ViewNewsArticleViewModel>>(
                newsArticles
            );

            var pageSize = 3;
            var paginatedList = PaginatedList<ViewNewsArticleViewModel>.Create(
                viewReportNewsArticleViewModel.AsQueryable(),
                pageNumber ?? 1,
                pageSize
            );

            return View(paginatedList);
        }
    }
}
