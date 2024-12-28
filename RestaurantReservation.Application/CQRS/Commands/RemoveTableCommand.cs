using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands;

/// <summary>
///     Removes a table from the system.
/// </summary>
public record RemoveTableCommand(Guid RestaurantId, Guid TableId) : IRequest<bool>;