using MediatR;
using RestaurantReservation.Application.CQRS.Queries;
using RestaurantReservation.Application.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.QueryHandlers;

public class GetAllRestaurantsQueryHandler(IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetAllRestaurantsQuery, List<RestaurantInfo>>
{
    public async Task<List<RestaurantInfo>> Handle(
        GetAllRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        var restaurants = await restaurantRepository.GetAllAsync(cancellationToken);

        return restaurants
            .Select(r => new RestaurantInfo
            {
                Id = r.Id,
                Name = r.Name,
                TableCount = r.Tables.Count
            })
            .ToList();
    }
}