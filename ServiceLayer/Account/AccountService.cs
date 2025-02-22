using AutoMapper;
using RepositoryLayer.Account;
using ServiceLayer.Models;

namespace ServiceLayer.Account;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<AccountDTO?> LoginAsync(string email, string password)
    {
        var systemAccount = await _accountRepository.Login(email, password);
        var accountDto = _mapper.Map<AccountDTO>(systemAccount);
        return accountDto;
    }
}
