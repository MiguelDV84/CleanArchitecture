using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users.Events;

public sealed record UserCreatedDomainEvents(Guid UserID) : IDomainEvent
{

}