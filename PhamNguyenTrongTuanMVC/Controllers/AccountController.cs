using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhamNguyenTrongTuanMVC.Models.Account;
using RepositoryLayer.Enums;
using ServiceLayer.Account;
using ServiceLayer.Models;

namespace PhamNguyenTrongTuanMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    private readonly AdminOptions _adminOption;

    public AccountController(
        IAccountService accountService,
        IOptions<AdminOptions> options,
        IMapper mapper
    )
    {
        _accountService = accountService;
        _mapper = mapper;
        _adminOption = options.Value;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var accountDtos = await _accountService.ListAllAccounts();
        var listAccountViewModel = _mapper.Map<IEnumerable<ViewAccountViewModel>>(accountDtos);
        return View(listAccountViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(
        [FromForm]
        [Bind("AccountName,AccountEmail,AccountRole")]
            AddNewAccountViewModel addAccountViewModel
    )
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_AddAccountModal", addAccountViewModel);
        }
        var accountDto = _mapper.Map<AccountDTO>(addAccountViewModel);
        await _accountService.CreateNewAccountAsync(accountDto);
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Add()
    {
        var addAccountViewModel = new AddNewAccountViewModel()
        {
            AccountEmail = "",
            AccountName = "",
        };
        return PartialView("_AddAccountModal", addAccountViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var accountDto = await _accountService.GetAcountByIdAsync(id);
        var updateAccountViewModel = _mapper.Map<UpdateAccountViewModel>(accountDto);

        return PartialView("_UpdateAccountModal", updateAccountViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateAccountViewModel updateAccountViewModel)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_UpdateAccountModal", updateAccountViewModel);
        }

        var accountDto = _mapper.Map<AccountDTO>(updateAccountViewModel);

        var updatedAccount = await _accountService.UpdateAccountAsync(accountDto);
        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteEffected = await _accountService.DeleteAccountAsync(id);
        return RedirectToAction("List");
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
