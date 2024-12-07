using Fonsion.be.Domain.PromoCodes;

namespace Fonsion.be.Application.Common.Interfaces;

public interface IReservationService
{
    PromoCode GeneratePromoCode();
    decimal CalculatePrice(DateTime fromDate, DateTime toDate, decimal pricePerNight, decimal discountPercentage = 0);
}