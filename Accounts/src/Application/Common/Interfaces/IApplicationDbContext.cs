using Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> DomainUsers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
