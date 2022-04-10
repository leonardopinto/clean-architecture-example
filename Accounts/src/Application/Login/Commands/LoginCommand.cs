using Accounts.Application.Common.Interfaces;
using Accounts.Domain.Entities;
using MediatR;

namespace Accounts.Application.Login.Commands;

public class LoginCommand : IRequest<LoginResult>
{
    public string? Email { get; set; }

    public string? Password { get; set; }   
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public LoginCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.PasswordSignInAsync(request.Email!, request.Password!);

        return new LoginResult(result.Succeeded, result.Token, result.Expiration);
    }
}
