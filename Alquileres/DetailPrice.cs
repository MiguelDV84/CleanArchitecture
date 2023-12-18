using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public record DetailPrice(
    Moneda PeriodPrice,
    Moneda Maintenance,
    Moneda Accesory,
    Moneda TotalPrice
);