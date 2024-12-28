using MediatR;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.CommandHandlers;

public class UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
    : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository
            .GetByIdAsync(request.RestaurantId, cancellationToken) ?? throw new NotFoundException();

        restaurant.Name = request.Name;
        restaurant.BufferTime = request.BufferTime;
        restaurant.OpeningTime = request.OpeningTime;
        restaurant.ClosingTime = request.ClosingTime;
        restaurant.DefaultReservationDuration = request.DefaultReservationDuration;
        
        restaurantRepository.Update(restaurant);
        await restaurantRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}