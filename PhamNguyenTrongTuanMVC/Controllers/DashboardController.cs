using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhamNguyenTrongTuanMVC.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Chart()
        {
            var email = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
            return View();
        }
    }
}
