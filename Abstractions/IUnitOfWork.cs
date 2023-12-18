using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}