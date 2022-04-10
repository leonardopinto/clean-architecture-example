using Accounts.Application.Common.Interfaces;
using Accounts.Application.Common.Security;
using Accounts.Domain.Entities;
using Accounts.Domain.Enums;
using MediatR;

namespace Accounts.Application.Users.Commands.CreateUser;

[Authorize(Roles = nameof(Role.Administrator))]
[Authorize(Policy ="CanCreate")]
public class CreateUserCommand : IRequest<CreateUserResult>
{
    public string? Name { get; set; }

    public string? Email { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User
        {
            Email = request.Email!,
            Name = request.Name!,
        };

        var result = await _identityService
            .CreateUserAsync(entity, cancellationToken);

        return new CreateUserResult(entity.Id, new Guid(result.UserIdentityId), result.NewPassword);
    }
}
