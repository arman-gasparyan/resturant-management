using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands;

/// <summary>
///     Adds a new table to an existing restaurant.
/// </summary>
public record AddTableToRestaurantCommand(Guid RestaurantId, string Location, int NumberOfSeats)
    : IRequest<Guid>;