namespace CleanArchitecture.Domain.Vehiculos;

public record TipoMoneda
{
    public static readonly TipoMoneda NONE = new(string.Empty);
    public static readonly TipoMoneda USD = new("USD");
    public static readonly TipoMoneda EUR = new("EUR");

    private TipoMoneda(string codigo)
    {
        Codigo = codigo;
    }

    public string? Codigo { get; init; }

    public static readonly IReadOnlyCollection<TipoMoneda> All = new List<TipoMoneda>
    {
        USD,
        EUR
    };

    public static TipoMoneda FromCodigo(string codigo)
    {
        return All.FirstOrDefault(x => x.Codigo == codigo) ?? 
        throw new ApplicationException("El tipo de moneda no es valido");
    }

}