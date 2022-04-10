namespace Accounts.Domain.Events;

public class UserCreatedEvent : DomainEvent
{
    public UserCreatedEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
