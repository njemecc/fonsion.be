using Fonsion.be.Domain.Entities;
using Fonsion.be.Domain.PromoCodes;
using Fonsion.be.Domain.Users;

namespace Fonsion.be.Domain.Reservations;

public class Reservation
{
    public Guid Id { get; set; }

    public DateTime FromDate { get; set; }
    
    public DateTime ToDate { get; set; }
    
    public decimal TotalPrice { get; set; }

    public Guid UserId { get; set; }
    
    public Guid RoomId { get; set; }
    
    public Guid? PromoCodeId { get; set; }
    
    
    public Room Room { get; set; }
    
    public PromoCode? PromoCode { get; set; }


}