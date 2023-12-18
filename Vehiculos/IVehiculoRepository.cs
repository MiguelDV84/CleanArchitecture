namespace CleanArchitecture.Domain.Vehiculos;

public interface IVehiculoRepository
{
    Task<Vehiculos> GetByIdAsybnc(Guid VehiculoID, CancellationToken cancellationToken);
}