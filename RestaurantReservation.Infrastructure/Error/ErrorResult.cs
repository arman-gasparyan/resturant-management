namespace RestaurantReservation.Infrastructure.Error;

public record ErrorResult
{
    public int StatusCode { get; set; }
}