using MediatR;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.CommandHandlers;

public class RemoveTableCommandHandler(ITableRepository tableRepository) : IRequestHandler<RemoveTableCommand, bool>
{
    public async Task<bool> Handle(RemoveTableCommand request, CancellationToken cancellationToken)
    {
        var table = await tableRepository.GetByIdAsync(request.TableId, cancellationToken);
        if (table == null) return false;

        tableRepository.Remove(table);
        await tableRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}