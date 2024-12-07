namespace Fonsion.be.Domain.PromoCodes;

public class PromoCode
{
    public Guid Id { get; set; }
    
    public decimal Value { get; set; }
    
    public bool Active { get; set; }
}