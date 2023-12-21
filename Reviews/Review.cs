using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rents;

namespace CleanArchitecture.Domain.Reviews;

public sealed class Review : Entity
{
    private Review (
        Guid id,
        Guid vehicleID, 
        Guid rentID, 
        Guid userID, 
        Rating rating, 
        Comment? comment, 
        DateTime? dateCreation
        ) : base(id)
    {
        VehicleID = vehicleID;
        RentID = rentID;
        UserID = userID;
        Rating = rating;
        Comment = comment;
        DateCreation = dateCreation;
    }
    public Guid VehicleID { get; private set; }
    public Guid RentID { get; private set; }
    public Guid UserID { get; private set; }
    public Rating Rating { get; private set; }
    public Comment? Comment { get; private set; }
    public DateTime? DateCreation { get; private set; }

    public static Result<Review> Create(
        Rent rent,
        Rating rating,
        Comment? comment,
        DateTime? dateCreation
    )
    {
        if(rent.Status != RentalStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(
            Guid.NewGuid(),
            rent.VehicleID,
            rent.ID,
            rent.UserID,
            rating,
            comment,
            dateCreation
        );

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.ID));

        return review;
    }
}