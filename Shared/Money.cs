namespace CleanArchitecture.Domain.Shared;

public record Money(decimal Cantidad, MoneyType MoneyType)
{
    public static Money operator +(Money primera, Money segunda)
    {
        if (primera.MoneyType != segunda.MoneyType)
        {
            throw new InvalidOperationException("El tipo de moneda debe ser el mismo");
        }

        return new Money(primera.Cantidad + segunda.Cantidad, primera.MoneyType);
    }

    public static Money Zero() => new(0, MoneyType.NONE);

    public static Money Zero(MoneyType moneyType) => new(0, moneyType);

    public bool IsZero() => this == Zero(MoneyType);

}