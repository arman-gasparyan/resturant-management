using MediatR;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.CommandHandlers;

public class UpdateTableCommandHandler(ITableRepository tableRepository) : IRequestHandler<UpdateTableCommand, bool>
{
    public async Task<bool> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        var table = await tableRepository
            .GetByIdAsync(request.TableId, cancellationToken) ?? throw new NotFoundException();
        ;

        var updatedTable = new Table(
            request.Location,
            request.NumberOfSeats);

        tableRepository.Update(updatedTable);
        await tableRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}