namespace CleanArchitecture.Domain.Vehiculos;

public interface IVehiculoRepository
{
    Task<Vehiculo> GetByIdAsybnc(Guid VehiculoID, CancellationToken cancellationToken);
}