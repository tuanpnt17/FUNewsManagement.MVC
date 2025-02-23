﻿using AutoMapper;
using RepositoryLayer.Accounts;
using RepositoryLayer.Entities;
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
        var systemAccount = await _accountRepository.GetAccountByEmailAsync(email);
        if (systemAccount == null)
        {
            return null;
        }
        if (systemAccount.AccountPassword != password)
        {
            return null;
        }
        var accountDto = _mapper.Map<AccountDTO>(systemAccount);
        return accountDto;
    }

    public async Task<AccountDTO?> GetAcountByEmailAsync(string email)
    {
        var systemAccount = await _accountRepository.GetAccountByEmailAsync(email);
        var accountDto = _mapper.Map<AccountDTO>(systemAccount);
        return accountDto;
    }

    public async Task<AccountDTO?> GetAcountByIdAsync(int accountId)
    {
        var systemAccount = await _accountRepository.GetAccountByIdAsync(accountId);
        var accountDto = _mapper.Map<AccountDTO>(systemAccount);
        return accountDto;
    }

    public async Task<AccountDTO> CreateNewAccountAsync(AccountDTO accountDto)
    {
        var systemAccount = _mapper.Map<SystemAccount>(accountDto);
        var addedAccount = await _accountRepository.CreateAccountAsync(systemAccount);
        var accountDtoToReturn = _mapper.Map<AccountDTO>(addedAccount);
        return accountDtoToReturn;
    }

    public async Task<int?> UpdateAccountAsync(AccountDTO accountDto)
    {
        var systemAccount = _mapper.Map<SystemAccount>(accountDto);
        var effectedRow = await _accountRepository.UpdateAccountAsync(systemAccount);
        return effectedRow;
    }

    public async Task<int?> DeleteAccountAsync(int accountId)
    {
        var systemAccount = await _accountRepository.GetAccountByIdAsync(accountId);
        var effectedRow = await _accountRepository.DeleteAccountAsync(systemAccount);
        return effectedRow;
    }

    public async Task<IEnumerable<AccountDTO>> ListAllAccounts()
    {
        var accounts = await _accountRepository.ListAllAsync();
        var accountDtos = _mapper.Map<IEnumerable<AccountDTO>>(accounts);
        return accountDtos;
    }
}
