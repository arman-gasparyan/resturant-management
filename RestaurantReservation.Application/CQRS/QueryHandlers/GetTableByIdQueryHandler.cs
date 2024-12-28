using MediatR;
using RestaurantReservation.Application.CQRS.Queries;
using RestaurantReservation.Application.Models;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.QueryHandlers;

public class GetTableByIdQueryHandler(ITableRepository tableRepository) : IRequestHandler<GetTableByIdQuery, TableInfo>
{
    public async Task<TableInfo> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
    {
        var table = await tableRepository
            .GetByIdAsync(request.TableId, cancellationToken) ?? throw new NotFoundException();

        return new TableInfo
        {
            Id = table.Id,
            Location = table.Location,
            NumberOfSeats = table.NumberOfSeats
        };
    }
}