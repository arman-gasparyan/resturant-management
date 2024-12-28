using System.Runtime.Serialization;

namespace RestaurantReservation.Domain.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context)
    {
    }
}