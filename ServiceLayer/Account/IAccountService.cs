using ServiceLayer.Models;

namespace ServiceLayer.Account;

public interface IAccountService
{
    Task<AccountDTO?> LoginAsync(string email, string password);
}
