namespace CleanArchitecture.Domain.Shared;

public record Moneda(decimal Cantidad, TipoMoneda TipoMoneda)
{
    public static Moneda operator +(Moneda primera, Moneda segunda)
    {
        if (primera.TipoMoneda != segunda.TipoMoneda)
        {
            throw new InvalidOperationException("El tipo de moneda debe ser el mismo");
        }

        return new Moneda(primera.Cantidad + segunda.Cantidad, primera.TipoMoneda);
    }

    public static Moneda Zero() => new(0, TipoMoneda.NONE);

    public static Moneda Zero(TipoMoneda tipoMoneda) => new(0, tipoMoneda);

    public bool IsZero() => this == Zero(TipoMoneda);

}