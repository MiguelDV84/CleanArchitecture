using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Alquileres.Events;

public static class AlquilerErrors
{
    public static Error NotFound = new Error(
        "Alquiler.NotFound",
        "El alquiler con el id especificado no fue encontrado"
    );

    public static Error Overlap = new Error(
        "Alquiler.Overlap",
        "El alquiler ya se realizó por otro cliente en la misma fecha"
    );

    public static Error NotReserved = new Error(
        "Alquiler.NotReserver",
        "El alquiler no pudo ser reservado"
    );

    public static Error NotConfirmed = new Error(
        "Alquiler.NotConfirmed",
        "El alquiler no pudo ser confirmado"
    );

    public static Error AlreadyStarted = new Error(
        "Alquiler.AlreadyStarted",
        "El alquiler ya fue iniciado"
    );

}