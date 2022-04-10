using Accounts.Domain.Common;

namespace Accounts.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
