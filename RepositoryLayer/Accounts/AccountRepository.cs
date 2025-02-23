using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FUNewsDBContext _context;

        public AccountRepository(FUNewsDBContext context)
        {
            _context = context;
        }

        public Task<SystemAccount?> GetAccountByEmailAsync(string email)
        {
            return _context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountEmail == email);
        }

        public Task<SystemAccount?> GetAccountByIdAsync(int id)
        {
            return _context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public async Task<IEnumerable<SystemAccount>> ListAllAsync()
        {
            return await _context.SystemAccounts.ToListAsync();
        }

        public async Task<SystemAccount> CreateAsync(SystemAccount account)
        {
            account.AccountPassword = "@1";
            var addedAccount = await _context.SystemAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return addedAccount.Entity;
        }

        public async Task<int?> UpdateAsync(SystemAccount account)
        {
            var systemAccount = await GetAccountByIdAsync(account.AccountId);
            if (systemAccount == null)
            {
                return null;
            }

            // Update fields that are different
            if (
                systemAccount.AccountName != account.AccountName
                && !string.IsNullOrEmpty(account.AccountName)
            )
            {
                systemAccount.AccountName = account.AccountName;
            }
            if (
                systemAccount.AccountEmail != account.AccountEmail
                && !string.IsNullOrEmpty(account.AccountEmail)
            )
            {
                systemAccount.AccountEmail = account.AccountEmail;
            }
            if (systemAccount.AccountRole != account.AccountRole)
            {
                systemAccount.AccountRole = account.AccountRole;
            }
            if (
                systemAccount.AccountPassword != account.AccountPassword
                && !string.IsNullOrEmpty(account.AccountPassword)
            )
            {
                systemAccount.AccountPassword = account.AccountPassword;
            }

            var effectedRow = await _context.SaveChangesAsync();
            return effectedRow;
        }

        public async Task<int?> DeleteAsync(SystemAccount? account)
        {
            if (account == null)
                return null;
            var systemAccount = await GetAccountByIdAsync(account.AccountId);
            if (systemAccount == null)
            {
                return null;
            }
            await Task.Run(() => _context.SystemAccounts.Remove(systemAccount));
            var effectedRow = await _context.SaveChangesAsync();
            return effectedRow;
        }
    }
}
