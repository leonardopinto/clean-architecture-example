namespace Accounts.Application.Users.Commands.CreateUser;
public class CreateUserResult
{
    public CreateUserResult(Guid id, Guid identityUserId, string password)
    {
        Id = id;
        Password = password;
        IdentityUserId = identityUserId;
    }

    public Guid Id { get; private set; }    

    public Guid IdentityUserId { get; private set; }
    public string Password { get; private set; }
}

