using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres.Events;
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
    public Moneda? Maintenance { get; private set;}
    public Moneda? Accesory { get; private set; }
    public Moneda? TotalPrice { get; private set;}
    public AlquilerStatus Status { get; private set; }
    public DateRange? Duration { get; private set; }
    public DateTime? DateCreation { get; private set; }
    public DateTime? DateConfirmation { get; private set; }
    public DateTime? DateDenial { get; private set; }
    public DateTime? DateCompleted { get; private set; }
    public DateTime? DateCancel { get; private set; }

    public static Alquiler Create(
        Guid vehicleID,
        Guid userID,
        DateRange duration,
        DateTime dateCreation,
        DetailPrice detailPrice
    ){
        var alquiler = new Alquiler(
            Guid.NewGuid(),
            vehicleID,
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

        return alquiler;
    }
}
