using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using Accounts.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Accounts.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _applicationDbContext;

    public ApplicationDbContextSeed(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _applicationDbContext = applicationDbContext;
    }

    public async Task SeedAdminUserAsync()
    {
        var admin = new ApplicationUser()
        {
            Email = "administrator@localhost.com",
            UserName = "administrator@localhost.com",
        };

        await SeedApplicationUserAsync(admin, "Administrator1!", Role.Administrator);
    }

    private async Task SeedApplicationUserAsync(ApplicationUser applicationUser, string password, Role role)
    {
        var identityRole = new IdentityRole(role.ToString());

        if (_roleManager.Roles.All(r => r.Name != identityRole.Name))
        {
            await _roleManager.CreateAsync(identityRole);
        }

        if (_userManager.Users.All(u => u.UserName != applicationUser.UserName))
        {
            await _userManager.CreateAsync(applicationUser, password);
            await _userManager.AddToRolesAsync(applicationUser, new[] { identityRole.Name });
        }
    }

    private async Task SeedApplicationUserAsync(ApplicationUser applicationUser, string password)
    {
        if (_userManager.Users.All(u => u.UserName != applicationUser.UserName))
        {
            await _userManager.CreateAsync(applicationUser, password);
        }
    }

    public async Task SeedSampleDataAsync()
    {
        // Seed, if necessary
        if (!_applicationDbContext.DomainUsers.Any())
        {
            var user = new User()
            {
                Email = "leonardosimoespinto@gmail.com",
                Name = "Leonardo Pinto"
            };

            await SeedApplicationUserAsync(new ApplicationUser(user), "MYPassword1234$1");

            _applicationDbContext.DomainUsers.Add(user);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
