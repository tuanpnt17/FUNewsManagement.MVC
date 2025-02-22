using RepositoryLayer.Data;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Accounts;

public interface IAccountRepository : IGenericRepository<SystemAccount>
{
    Task<SystemAccount?> GetAccountByIdAsync(int id);
    Task<SystemAccount?> GetAccountByEmailAsync(string email);
}
