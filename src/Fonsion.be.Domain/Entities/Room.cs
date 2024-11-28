namespace Fonsion.be.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    
    public String Name { get; set; }
    
    public int Capacity { get; set; }
    
    public decimal PricePerNight { get; set; }
    
    public String Description { get; set; }

    public String ImageUrl { get; set; }
}