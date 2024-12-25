using Fonsion.be.Domain.Images;
using Fonsion.be.Domain.Reservations;

namespace Fonsion.be.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    
    public String Name { get; set; }
    
    public int Capacity { get; set; }
    
    public decimal PricePerNight { get; set; }
    
    public String Description { get; set; }
    
    public ICollection<Image> Images { get; set; }  
    
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}