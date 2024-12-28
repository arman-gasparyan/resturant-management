namespace RestaurantReservation.Application.Models;

/// <summary>
///     Represents a minimal model for table information.
/// </summary>
public class TableInfo
{
    public Guid Id { get; set; }
    public string Location { get; set; }
    public int NumberOfSeats { get; set; }
}