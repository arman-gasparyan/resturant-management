using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands
{
    /// <summary>
    /// Command to create a new reservation for a specific table.
    /// </summary>
    /// <param name="TableId">The unique identifier of the table to be reserved.</param>
    /// <param name="ReservationTime">The desired start time for the reservation (in UTC).</param>
    /// <param name="Email">The email address of the person making the reservation.</param>
    /// <param name="NumberOfVisitors">The number of guests for the reservation.</param>
    /// <param name="Duration">The duration of the reservation. If not specified, the restaurant's default duration is used.</param>
    public record CreateReservationCommand(
        Guid TableId,
        DateTime ReservationTime,
        string Email,
        int NumberOfVisitors,
        TimeSpan Duration) : IRequest<Guid>;
}