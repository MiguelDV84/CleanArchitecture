using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public sealed class Alquiler : Entity
{
    private Alquiler(
        Guid id,
        Guid vehicleID,
        Guid userID,
        Moneda price,
        Moneda maintenance,
        Moneda totalPrice,
        Moneda accesory,
        DateRange duration,
        DateTime dateCreation,
        AlquilerStatus status
        ) : base(id)
    {
        VehicleID = vehicleID;
        UserID = userID;
        Price = price;
        Maintenance = maintenance;
        TotalPrice = totalPrice;
        Accesory = accesory;
        Duration = duration;
        DateCreation = dateCreation;
        Status = status;
    }

    public Guid VehicleID { get; private set; }
    public Guid UserID { get; private set; }
    public Moneda? Price { get; private set; }
    public Moneda? Maintenance { get; private set; }
    public Moneda? Accesory { get; private set; }
    public Moneda? TotalPrice { get; private set; }
    public AlquilerStatus Status { get; private set; }
    public DateRange? Duration { get; private set; }
    public DateTime? DateCreation { get; private set; }
    public DateTime? DateConfirmation { get; private set; }
    public DateTime? DateDenial { get; private set; }
    public DateTime? DateCompleted { get; private set; }
    public DateTime? DateCancel { get; private set; }

    public static Alquiler Create(
        Vehiculo vehicle,
        Guid userID,
        DateRange duration,
        DateTime dateCreation,
        PriceService priceService
    )
    {
        var detailPrice = priceService.CalculatePrice(
            vehicle,
            duration
        );

        var alquiler = new Alquiler(
            Guid.NewGuid(),
            vehicle.ID,
            userID,
            detailPrice.PeriodPrice,
            detailPrice.Maintenance,
            detailPrice.Accesory,
            detailPrice.TotalPrice,
            duration,
            dateCreation,
            AlquilerStatus.Reservado
        );

        alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.ID));

        vehicle.FechaUltimoAlquiler = dateCreation;

        return alquiler;
    }

    public Result Confirmation(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);
        }

        Status = AlquilerStatus.Confirmado;
        DateConfirmation = utcNow;

        RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(ID));

        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Reservado)
        {
            return Result.Failure(AlquilerErrors.NotReserved);
        }

        Status = AlquilerStatus.Rechazado;
        DateDenial = utcNow;

        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(ID));

        return Result.Success();
    }

    public Result Cencelled(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmed);
        }

        var currentDate = DateOnly.FromDateTime(utcNow);

        if (currentDate > Duration!.InitialDate)
        {
            return Result.Failure(AlquilerErrors.AlreadyStarted);
        }

        Status = AlquilerStatus.Cancelado;
        DateCancel = utcNow;

        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(ID));

        return Result.Success();
    }

    public Result Completed(DateTime utcNow)
    {
        if (Status != AlquilerStatus.Confirmado)
        {
            return Result.Failure(AlquilerErrors.NotConfirmed);
        }

        Status = AlquilerStatus.Completado;
        DateCompleted = utcNow;

        RaiseDomainEvent(new AlquilerCompletadoDomainEvent(ID));

        return Result.Success();
    }
}
