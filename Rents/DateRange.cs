namespace CleanArchitecture.Domain.Rents;

public sealed record DateRange
{
    private DateRange(
        DateOnly initialDate,
        DateOnly finishDate
    )
    {
        InitialDate = initialDate;
        FinishDate = finishDate;
    }

    public DateOnly InitialDate { get; private set; }
    public DateOnly FinishDate { get; private set; }
    public int Days => FinishDate.DayNumber - InitialDate.DayNumber;

    public static DateRange Create(DateOnly initialDate, DateOnly finishDate)
    {
        if(finishDate > initialDate)
        {
            throw new ApplicationException("La fecha final no puede ser mayor que la fecha de inicio");
        }

        var dateRange = new DateRange(
            initialDate,
            finishDate
        );

        return dateRange;
    }

}