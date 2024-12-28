using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Application.Models;
    
public class ReservationInfo
{
    public Guid Id { get; private set; }
    public DateTime ReservationTime { get; private set; }
    public string? Email { get; private set; }
    public int NumberOfVisitors { get; private set; }
    public Guid TableId { get; private set; }
    public Table? Table { get; private set; }
    public TimeSpan Duration { get; private set; }

    [Timestamp]
    public byte[]? RowVersion { get; private set; }

    [NotMapped]
    public DateTime EndTime => ReservationTime.Add(Duration);
}