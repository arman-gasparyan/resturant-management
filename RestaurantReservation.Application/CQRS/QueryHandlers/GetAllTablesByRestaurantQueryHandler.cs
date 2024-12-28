using MediatR;
using RestaurantReservation.Application.CQRS.Queries;
using RestaurantReservation.Application.Models;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.QueryHandlers;

public class GetAllTablesByRestaurantQueryHandler(IRestaurantRepository restaurantRepository)
    : IRequestHandler<GetAllTablesQuery, List<TableInfo>>
{
    public async Task<List<TableInfo>> Handle(GetAllTablesQuery request,
        CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository
            .GetByIdAsync(request.RestaurantId, cancellationToken) ?? throw new NotFoundException();

        var tables = restaurant.Tables;

        return tables.Select(t => new TableInfo
        {
            Id = t.Id,
            Location = t.Location,
            NumberOfSeats = t.NumberOfSeats
        }).ToList();
    }
}