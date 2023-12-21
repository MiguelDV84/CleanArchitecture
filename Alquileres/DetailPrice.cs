using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Alquileres;

public record DetailPrice(
    Moneda PeriodPrice,
    Moneda Maintenance,
    Moneda Accesory,
    Moneda TotalPrice
);