using MediatR;
using RestaurantReservation.Application.CQRS.Queries;
using RestaurantReservation.Application.Models;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.QueryHandlers;

public class GetRestaurantByIdQueryHandler(IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetRestaurantByIdQuery, RestaurantInfo>
{
    public async Task<RestaurantInfo> Handle(
        GetRestaurantByIdQuery request,
        CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository
            .GetByIdAsync(request.RestaurantId, cancellationToken) ?? throw new NotFoundException();

        return new RestaurantInfo
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            TableCount = restaurant.Tables.Count
        };
    }
}