using MediatR;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.CommandHandlers;

public class AddTableToRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
    : IRequestHandler<AddTableToRestaurantCommand, Guid>
{
    public async Task<Guid> Handle(AddTableToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository
            .GetByIdAsync(request.RestaurantId, cancellationToken) ?? throw new NotFoundException();

        var newTable = new Table(
            request.Location,
            request.NumberOfSeats);

        restaurant.AddTable(newTable);
        restaurantRepository.Update(restaurant);
        await restaurantRepository.SaveChangesAsync(cancellationToken);

        return newTable.Id;
    }
}