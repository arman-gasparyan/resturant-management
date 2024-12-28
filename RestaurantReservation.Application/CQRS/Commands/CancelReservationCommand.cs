using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands;

/// <summary>
///     Cancels (removes) a reservation.
/// </summary>
public record CancelReservationCommand(Guid ReservationId) : IRequest<bool>;