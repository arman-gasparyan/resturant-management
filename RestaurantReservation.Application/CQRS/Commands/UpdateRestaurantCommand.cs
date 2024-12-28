using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands;

/// <summary>
///     Updates the name (or other properties) of an existing Restaurant.
/// </summary>
public record UpdateRestaurantCommand(
    Guid RestaurantId, 
    string Name, 
    TimeSpan OpeningTime, 
    TimeSpan ClosingTime, 
    TimeSpan DefaultReservationDuration, 
    TimeSpan BufferTime
) : IRequest<bool>;