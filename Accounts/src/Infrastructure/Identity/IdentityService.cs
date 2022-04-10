using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Accounts.Application.Common.Interfaces;
using Accounts.Application.Common.Models;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using Accounts.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IApplicationDbContext context,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> GetUserNameAsync(string userIdentityId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userIdentityId);

        return user.UserName;
    }

    public async Task<(Result Result, string UserIdentityId)> CreateUserAsync(string userName, string password)
    {
        return await CreateUserAsync(userName, password, null);
    }

    public async Task<(Result Result, string UserIdentityId, string NewPassword)> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        var userApp = new ApplicationUser(user);

        // TO DO: Generate password
        var newPassword = $"MyPassword${ new Random().Next(12, 2023) }";

        var result = await _userManager.CreateAsync(userApp, newPassword);

        user.IdentityUserId = userApp.Id;

        user.DomainEvents.Add(new UserCreatedEvent(user));

        _context.DomainUsers.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return (result.ToApplicationResult(), userApp.Id, newPassword);
    }

    public async Task<(bool Succeeded, string? Token, DateTime? Expiration)> PasswordSignInAsync(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var userRoles = await _userManager.GetRolesAsync(user!);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user!.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return (true, new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }

        return (false, null, null);
    }

    public async Task<(Result Result, string UserIdentityId)> CreateUserAsync(string userName, string password, Role[]? roles)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (roles?.Any() == true)
        {
            await _userManager.AddToRolesAsync(user, roles.Select(r => r.ToString()));
        }

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userName, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userName, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userIdentityId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userIdentityId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
