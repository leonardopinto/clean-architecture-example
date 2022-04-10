using Accounts.Application.Common.Models;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;

namespace Accounts.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userIdentityId);

    Task<bool> IsInRoleAsync(string userName, string role);

    Task<bool> AuthorizeAsync(string userName, string policyName);

    Task<(Result Result, string UserIdentityId)> CreateUserAsync(string userName, string password);

    Task<(Result Result, string UserIdentityId)> CreateUserAsync(string userName, string password, Role[]? roles);

    Task<(Result Result, string UserIdentityId, string NewPassword)> CreateUserAsync(User user, CancellationToken cancellationToken);

    Task<Result> DeleteUserAsync(string userIdentityId);

    Task<(bool Succeeded, string? Token, DateTime? Expiration)> PasswordSignInAsync(string email, string password);
}
