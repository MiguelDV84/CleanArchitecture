namespace CleanArchitecture.Domain.Users;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid UserID, CancellationToken cancellationToken = default);
    void Add(User user);
}