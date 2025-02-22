using RepositoryLayer.Entities;

namespace RepositoryLayer.Account;

public interface IAccountRepository
{
    Task<SystemAccount?> Login(string email, string password);
}
