namespace Accounts.Application.Login.Commands;
public class LoginResult
{
    public LoginResult(bool succeeded, string? token, DateTime? expiration)
    {
        Succeeded = succeeded;
        Token = token;
        Expiration = expiration;   
    }

    public bool Succeeded { get; private set; } 
    public string? Token { get; private set; }
    public DateTime? Expiration { get; private set; }

}

