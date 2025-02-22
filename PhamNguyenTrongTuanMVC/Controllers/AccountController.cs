using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhamNguyenTrongTuanMVC.Models.Account;
using RepositoryLayer.Enums;
using ServiceLayer.Account;

namespace PhamNguyenTrongTuanMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly AdminOptions _adminOption;

        public AccountController(IAccountService accountService, IOptions<AdminOptions> options)
        {
            _accountService = accountService;
            _adminOption = options.Value;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAccountViewModel loginAccountViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginAccountViewModel);
            }

            if (_adminOption.Email != loginAccountViewModel.Email)
            {
                var accountDto = await _accountService.LoginAsync(
                    loginAccountViewModel.Email,
                    loginAccountViewModel.Password
                );

                if (accountDto == null)
                {
                    ModelState.AddModelError("Email", "Email or password is incorrect");
                    return View(loginAccountViewModel);
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Sid, accountDto.AccountEmail),
                    new(ClaimTypes.Name, accountDto.AccountName),
                    new(ClaimTypes.Email, accountDto.AccountEmail),
                    new(ClaimTypes.Role, accountDto.AccountRole.ToString()),
                };

                var principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)
                );
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal
                );
                if (accountDto.AccountRole == AccountRole.Staff)
                {
                    return RedirectToAction("Chart", "Dashboard");
                }

                return RedirectToAction("Index", "NewsArticle");
            }

            if (_adminOption.Password != loginAccountViewModel.Password)
            {
                ModelState.AddModelError("Password", "Email or password is incorrect");
                return View(loginAccountViewModel);
            }

            var adminClaims = new List<Claim>
            {
                new(ClaimTypes.Sid, _adminOption.Email),
                new(ClaimTypes.Name, "System Admin"),
                new(ClaimTypes.Email, _adminOption.Email),
                new(ClaimTypes.Role, AccountRole.Admin.ToString()),
            };

            var adminPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme)
            );
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                adminPrincipal
            );
            return RedirectToAction("Chart", "Dashboard");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            return View();
        }
    }
}
