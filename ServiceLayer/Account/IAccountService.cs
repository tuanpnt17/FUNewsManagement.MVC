using ServiceLayer.Models;

namespace ServiceLayer.Account;

public interface IAccountService
{
    Task<AccountDTO?> LoginAsync(string email, string password);

    Task<AccountDTO?> GetAcountByEmailAsync(string email);

    Task<AccountDTO?> GetAcountByIdAsync(int accountId);

    Task<AccountDTO> CreateNewAccountAsync(AccountDTO accountDto);
    Task<AccountDTO> UpdateAccountAsync(AccountDTO accountDto);
    Task<int?> DeleteAccountAsync(AccountDTO accountDTO);
}
