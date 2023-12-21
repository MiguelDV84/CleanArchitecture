using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public sealed record ReviewCreatedDomainEvent(Guid AlquilerID) : IDomainEvent
{}