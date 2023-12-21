
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Rents;

public sealed class PriceService
{
    public DetailPrice CalculatePrice(Vehicle vehicle, DateRange period)
    {
        var moneyType = vehicle.Price!.MoneyType;

        var pricePeriod = new Money(period.Days * vehicle.Price.Cantidad, moneyType);

        decimal porcentageChange = 0;

        foreach (Accesory accesory in vehicle.Accesorys)
        {
            porcentageChange += accesory switch
            {
                Accesory.AppleCar or Accesory.AndroidCar => 0.05m,
                Accesory.AirConditioning => 0.01m,
                Accesory.Maps => 0.01m,
                Accesory.Wifi => 0.10m,
                _ => 0
            };
        }

        var accesoryCharges = Money.Zero(moneyType);

        if(porcentageChange > 0)
        {
            accesoryCharges = new Money(pricePeriod.Cantidad * porcentageChange , moneyType);
        }

        var totalPrice = Money.Zero();
        totalPrice += pricePeriod;

        if(!vehicle!.Maintenance!.IsZero())
        {
            totalPrice += vehicle.Maintenance;
        }

        totalPrice += accesoryCharges;

        return new DetailPrice(pricePeriod, vehicle.Maintenance, accesoryCharges, totalPrice);
    }
}