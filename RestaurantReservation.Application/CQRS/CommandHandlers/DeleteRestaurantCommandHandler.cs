using MediatR;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.CommandHandlers;

public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
    : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository
            .GetByIdAsync(request.RestaurantId, cancellationToken) ?? throw new NotFoundException();

        restaurantRepository.Remove(restaurant);
        await restaurantRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}