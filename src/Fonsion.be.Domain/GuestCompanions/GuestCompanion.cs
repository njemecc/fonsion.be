using Fonsion.be.Domain.Reservations;

namespace Fonsion.be.Domain.GuestCompanion;

public class GuestCompanion
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public Guid ReservationId { get; set; }
    
    public Reservation Reservation { get; set; }
}