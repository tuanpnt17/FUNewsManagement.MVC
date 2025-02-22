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

        public async Task<SystemAccount> CreateAccountAsync(SystemAccount newsArticle)
        {
            var addedAccount = await _context.SystemAccounts.AddAsync(newsArticle);
            await _context.SaveChangesAsync();
            return addedAccount.Entity;
        }

        public async Task<int?> UpdateAccountAsync(SystemAccount account)
        {
            var systemAccount = await GetAccountByIdAsync(account.AccountId);
            if (systemAccount == null)
            {
                return null;
            }
            await Task.Run(() => _context.SystemAccounts.Update(systemAccount));
            var effectedRow = await _context.SaveChangesAsync();
            return effectedRow;
        }

        public async Task<int?> DeleteAccountAsync(SystemAccount account)
        {
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
