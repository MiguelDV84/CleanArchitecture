using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public interface IAlquilerRepository
{
    Task<Alquiler?> GetByIdAsync(Guid ID, CancellationToken cancellationToken = default);
    Task<bool> IsOverlappingAsync(Vehiculo vehicle, DateRange dateRange, CancellationToken cancellationToken = default);
    void Add(Alquiler rent);
}