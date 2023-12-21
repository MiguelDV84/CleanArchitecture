
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public sealed class PriceService
{
    public DetailPrice CalculatePrice(Vehiculo vehicle, DateRange period)
    {
        var tipoMoneda = vehicle.Precio!.TipoMoneda;

        var pricePeriod = new Moneda(period.Days * vehicle.Precio.Cantidad, tipoMoneda);

        decimal porcentageChange = 0;

        foreach (Accesorio accesory in vehicle.Accesorios)
        {
            porcentageChange += accesory switch
            {
                Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.01m,
                Accesorio.Wifi => 0.10m,
                _ => 0
            };
        }

        var accesoryCharges = Moneda.Zero(tipoMoneda);

        if(porcentageChange > 0)
        {
            accesoryCharges = new Moneda(pricePeriod.Cantidad * porcentageChange , tipoMoneda);
        }

        var totalPrice = Moneda.Zero();
        totalPrice += pricePeriod;

        if(!vehicle!.Mantenimiento!.IsZero())
        {
            totalPrice += vehicle.Mantenimiento;
        }

        totalPrice += accesoryCharges;

        return new DetailPrice(pricePeriod, vehicle.Mantenimiento, accesoryCharges, totalPrice);
    }
}