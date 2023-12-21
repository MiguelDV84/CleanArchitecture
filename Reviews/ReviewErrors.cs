using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public static class ReviewErrors {
     public static readonly Error NotEligible = new Error(
        "ReviewErrors.NotEligible",
        "Esta review no se puede realizar porque el alquiler no se completó."
    );
}