using MediatR;
using RestaurantReservation.Application.Models;

namespace RestaurantReservation.Application.CQRS.Queries;

public record GetAllTablesQuery(Guid RestaurantId) : IRequest<List<TableInfo>>;