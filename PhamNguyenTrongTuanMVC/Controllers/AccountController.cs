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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> List(
        string? searchString,
        string currentFilter,
        int? pageNumber
    )
    {
        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        ViewData["CurrentFilter"] = searchString;
        var accountDtos = await _accountService.ListAllAccounts();
        if (!string.IsNullOrEmpty(searchString))
        {
            accountDtos = accountDtos.Where(a =>
                a.AccountName.Contains(searchString) || a.AccountEmail.Contains(searchString)
            );
        }

        var listAccountViewModel = _mapper.Map<IEnumerable<ViewAccountViewModel>>(accountDtos);

        var pageSize = 3;
        var paginatedList = PaginatedList<ViewAccountViewModel>.Create(
            listAccountViewModel.AsQueryable(),
            pageNumber ?? 1,
            pageSize
        );
        return View(paginatedList);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
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
        var result = await _accountService.CreateNewAccountAsync(accountDto);
        if (result == null)
        {
            ModelState.AddModelError("AccountEmail", "Email is already exited");
            return PartialView("_AddAccountModal", addAccountViewModel);
        }
        return RedirectToAction("List");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var accountDto = await _accountService.GetAcountByIdAsync(id);
        var updateAccountViewModel = _mapper.Map<UpdateAccountViewModel>(accountDto);

        return PartialView("_UpdateAccountModal", updateAccountViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(UpdateAccountViewModel updateAccountViewModel)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_UpdateAccountModal", updateAccountViewModel);
        }

        // Todo: Update check email
        var accountDto = _mapper.Map<AccountDTO>(updateAccountViewModel);

        var updatedAccount = await _accountService.UpdateAccountAsync(accountDto);
        return RedirectToAction("List");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
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
                new(ClaimTypes.Sid, accountDto.AccountId.ToString()),
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
                return RedirectToAction("List", "Category");
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
            new(ClaimTypes.Name, _adminOption.Name),
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
        return RedirectToAction("Report", "Dashboard");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [Authorize(Roles = "Staff")]
    public async Task<IActionResult> Profile()
    {
        return View();
    }
}
