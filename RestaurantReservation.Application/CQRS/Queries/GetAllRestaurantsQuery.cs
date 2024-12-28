using MediatR;
using RestaurantReservation.Application.Models;

namespace RestaurantReservation.Application.CQRS.Queries;

public record GetAllRestaurantsQuery : IRequest<List<RestaurantInfo>>;