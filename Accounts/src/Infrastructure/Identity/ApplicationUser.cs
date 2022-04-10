using Accounts.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Accounts.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
    }

    public ApplicationUser(User user)
    {
        Email = user.Email;
        UserName = user.Email;
    }
}
