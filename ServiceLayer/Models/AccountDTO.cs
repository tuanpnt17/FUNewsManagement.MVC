using RepositoryLayer.Enums;

namespace ServiceLayer.Models;

public class AccountDTO
{
    public int AccountId { get; set; }
    public required string AccountName { get; set; }
    public required string AccountEmail { get; set; }
    public AccountRole AccountRole { get; set; }
    public required string AccountPassword { get; set; }
}
