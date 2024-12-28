using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands;

/// <summary>
///     Deletes a Restaurant by its Id.
/// </summary>
public record DeleteRestaurantCommand(Guid RestaurantId) : IRequest<bool>;