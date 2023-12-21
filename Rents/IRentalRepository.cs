using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Rents;

public interface IRentalRepository
{
    Task<Rent?> GetByIdAsync(Guid ID, CancellationToken cancellationToken = default);
    Task<bool> IsOverlappingAsync(Vehiculo vehicle, DateRange dateRange, CancellationToken cancellationToken = default);
    void Add(Rent rent);
}