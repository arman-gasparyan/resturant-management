using MediatR;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.Commands;

public class CancelReservationCommandHandler(IReservationRepository reservationRepository)
    : IRequestHandler<CancelReservationCommand, bool>
{
    public async Task<bool> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);
        if (reservation == null) return false;

        reservationRepository.Remove(reservation);
        await reservationRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}