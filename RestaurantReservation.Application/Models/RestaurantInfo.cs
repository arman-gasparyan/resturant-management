namespace RestaurantReservation.Application.Models;

/// <summary>
///     A simplified model returned to clients or used in queries/commands.
/// </summary>
public class RestaurantInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int TableCount { get; set; }
}