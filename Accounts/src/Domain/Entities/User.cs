namespace Accounts.Domain.Entities;

public class User : AuditableEntity, IHasDomainEvent
{
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;   

    public string Email { get; set; } = String.Empty;

    public string IdentityUserId { get; set; } = String.Empty;

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
