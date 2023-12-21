namespace CleanArchitecture.Domain.Abstractions;

public record Error(string Code, string Description)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error Null = new("Error.NullValue", "No se aceptan valores null");
}