namespace CleanArchitecture.Domain.Shared;

public record MoneyType
{
    public static readonly MoneyType NONE = new(string.Empty);
    public static readonly MoneyType USD = new("USD");
    public static readonly MoneyType EUR = new("EUR");

    private MoneyType(string codigo)
    {
        Codigo = codigo;
    }

    public string? Codigo { get; init; }

    public static readonly IReadOnlyCollection<MoneyType> All = new List<MoneyType>
    {
        USD,
        EUR
    };

    public static MoneyType FromCodigo(string codigo)
    {
        return All.FirstOrDefault(x => x.Codigo == codigo) ??
        throw new ApplicationException("El tipo de moneda no es valido");
    }

}