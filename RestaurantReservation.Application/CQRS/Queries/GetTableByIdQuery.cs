using MediatR;
using RestaurantReservation.Application.Models;

namespace RestaurantReservation.Application.CQRS.Queries;

public record GetTableByIdQuery(Guid ResturantId, Guid TableId) : IRequest<TableInfo>;