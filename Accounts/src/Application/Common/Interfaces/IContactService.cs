namespace Accounts.Application.Common.Interfaces;
public interface IContactService
{
    Task SendEmailAsync(string email, CancellationToken cancellationToken);
}
