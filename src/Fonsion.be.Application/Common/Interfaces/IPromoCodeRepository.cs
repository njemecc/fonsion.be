using Fonsion.be.Domain.PromoCodes;

namespace Fonsion.be.Application.Common.Interfaces;

public interface IPromoCodeRepository
{
    Task<PromoCode?> GetPromoCodeByIdAsync(Guid? promoCodeId);

    Task AddPromoCodeAsync(PromoCode? promoCode);

    Task UpdatePromoCodeAsync(PromoCode promoCode);
}