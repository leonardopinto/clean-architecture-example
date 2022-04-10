using Accounts.Application.Common.Interfaces;
using Accounts.Application.Common.Models;
using Accounts.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.Users.EventHandlers;

public class UserCreatedEventHandler : INotificationHandler<DomainEventNotification<UserCreatedEvent>>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;
    private readonly IContactService _contactService;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger, IContactService contactService)
    {
        _logger = logger;
        _contactService = contactService;

    }

    public async Task Handle(DomainEventNotification<UserCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("Accounts Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        await _contactService.SendEmailAsync(domainEvent.Item.Email, cancellationToken);
    }
}
