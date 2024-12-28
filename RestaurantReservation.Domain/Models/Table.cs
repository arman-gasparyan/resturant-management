namespace RestaurantReservation.Domain.Models;

/// <summary>
///     Represents a physical or virtual table in a restaurant.
/// </summary>
public class Table
{
    public Table()
    {
    }

    public Table(string location, int numberOfSeats)
    {
        Location = location;
        NumberOfSeats = numberOfSeats;
    }

    public Guid Id { get; private set; }
    public Guid RestaurantId { get; private set; }

    /// <summary>
    ///     Location (e.g., "Patio", "Main Hall", "Rooftop").
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    ///     Number of seats for this table.
    /// </summary>
    public int NumberOfSeats { get; private set; }
    public Restaurant Restaurant { get; private set; }
    public List<Reservation> Reservations { get; private set; }
}