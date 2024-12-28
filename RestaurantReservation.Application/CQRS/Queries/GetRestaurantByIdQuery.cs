using MediatR;
using RestaurantReservation.Application.Models;

namespace RestaurantReservation.Application.CQRS.Queries;

public record GetRestaurantByIdQuery(Guid RestaurantId)
    : IRequest<RestaurantInfo>;