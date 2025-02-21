using System.Diagnostics;
using FUNewsManagementSystem.Presentation.Models;
using FUNewsManagementSystem.RepositoryLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FUNewsDBContext _dBContext;

        public HomeController(ILogger<HomeController> logger, FUNewsDBContext dBContext)
        {
            _logger = logger;
            _dBContext = dBContext;
        }

        public IActionResult Index()
        {
            var newsArticles = _dBContext.NewsArticles.Include(n => n.Category).ToList();

            return View(newsArticles.Count());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                }
            );
        }
    }
}
