using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rents;

public record DetailPrice(
    Money PeriodPrice,
    Money Maintenance,
    Money Accesory,
    Money TotalPrice
);