using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FUNewsDBContext _context;

        public AccountRepository(FUNewsDBContext context)
        {
            _context = context;
        }

        public Task<SystemAccount?> Login(string email, string password)
        {
            return _context.SystemAccounts.FirstOrDefaultAsync(x =>
                x.AccountEmail == email && x.AccountPassword == password
            );
        }
    }
}
