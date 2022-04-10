using Accounts.Application.Common.Interfaces;

namespace Accounts.Infrastructure.Services;
public class ContactService : IContactService
{
    public Task SendEmailAsync(string email, CancellationToken cancellationToken)
    {
        // TODO: Send an email to the contact services. Apply Kafka, RabbitMQ, or a microservices messaging broker.
        return Task.CompletedTask;
    }
}
