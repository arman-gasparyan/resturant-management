using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands;

/// <summary>
///     Updates a table's properties (location, seats, etc.)
/// </summary>
public record UpdateTableCommand(Guid RestaurantId, Guid TableId, string Location, int NumberOfSeats)
    : IRequest<bool>;