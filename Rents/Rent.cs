using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Rents;

public sealed class Rent : Entity
{
    private Rent(
        Guid id,
        Guid vehicleID,
        Guid userID,
        Money price,
        Money maintenance,
        Money totalPrice,
        Money accesory,
        DateRange duration,
        DateTime dateCreation,
        RentalStatus status
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
    public Money? Price { get; private set; }
    public Money? Maintenance { get; private set; }
    public Money? Accesory { get; private set; }
    public Money? TotalPrice { get; private set; }
    public RentalStatus Status { get; private set; }
    public DateRange? Duration { get; private set; }
    public DateTime? DateCreation { get; private set; }
    public DateTime? DateConfirmation { get; private set; }
    public DateTime? DateDenial { get; private set; }
    public DateTime? DateCompleted { get; private set; }
    public DateTime? DateCancel { get; private set; }

    public static Rent Create(
        Vehicle vehicle,
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

        var rent = new Rent(
            Guid.NewGuid(),
            vehicle.ID,
            userID,
            detailPrice.PeriodPrice,
            detailPrice.Maintenance,
            detailPrice.Accesory,
            detailPrice.TotalPrice,
            duration,
            dateCreation,
            RentalStatus.Reserved
        );

        rent.RaiseDomainEvent(new AlquilerReservadoDomainEvent(rent.ID));

        vehicle.DateLastRent = dateCreation;

        return rent;
    }

    public Result Confirmation(DateTime utcNow)
    {
        if (Status != RentalStatus.Reserved)
        {
            return Result.Failure(RentalErrors.NotReserved);
        }

        Status = RentalStatus.Confirmed;
        DateConfirmation = utcNow;

        RaiseDomainEvent(new AlquilerConfirmadoDomainEvent(ID));

        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != RentalStatus.Reserved)
        {
            return Result.Failure(RentalErrors.NotReserved);
        }

        Status = RentalStatus.Rejected;
        DateDenial = utcNow;

        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(ID));

        return Result.Success();
    }

    public Result Cencelled(DateTime utcNow)
    {
        if (Status != RentalStatus.Confirmed)
        {
            return Result.Failure(RentalErrors.NotConfirmed);
        }

        var currentDate = DateOnly.FromDateTime(utcNow);

        if (currentDate > Duration!.InitialDate)
        {
            return Result.Failure(RentalErrors.AlreadyStarted);
        }

        Status = RentalStatus.Cancelled;
        DateCancel = utcNow;

        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(ID));

        return Result.Success();
    }

    public Result Completed(DateTime utcNow)
    {
        if (Status != RentalStatus.Confirmed)
        {
            return Result.Failure(RentalErrors.NotConfirmed);
        }

        Status = RentalStatus.Completed;
        DateCompleted = utcNow;

        RaiseDomainEvent(new AlquilerCompletadoDomainEvent(ID));

        return Result.Success();
    }
}
