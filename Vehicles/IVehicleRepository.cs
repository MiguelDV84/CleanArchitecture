namespace CleanArchitecture.Domain.Vehicles;

public interface IVehiculoRepository
{
    Task<Vehicle> GetByIdAsybnc(Guid VehicleID, CancellationToken cancellationToken);
}