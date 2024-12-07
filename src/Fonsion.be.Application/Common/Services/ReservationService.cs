using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.PromoCodes;

namespace Fonsion.be.Application.Common.Services;

public class ReservationService : IReservationService
{
    public decimal CalculatePrice(DateTime fromDate, DateTime toDate, decimal pricePerNight, decimal discountPercentage = 0)
    {
        var totalDays = (toDate - fromDate).Days;

        var basePrice = totalDays * pricePerNight;

        var discount = basePrice * (discountPercentage / 100);

        return basePrice - discount;
    }
    
    public PromoCode  GeneratePromoCode()
    {
        var discountOptions = new[] { 5, 10, 15, 20 };
        var random = new Random();
        var selectedDiscount = discountOptions[random.Next(discountOptions.Length)];

        return new PromoCode
        {
            Value = selectedDiscount,
            Active = true
        };
    }
}