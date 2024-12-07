using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.PromoCodes;
using Fonsion.be.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fonsion.be.Infrastructure.PromoCodes.Persistance;

public class PromoCodeRepository : IPromoCodeRepository

{
    private readonly FonsionDbContext _dbContext;

    public PromoCodeRepository(FonsionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PromoCode?> GetPromoCodeByIdAsync(Guid? promoCodeId)
    {
        return await _dbContext.PromoCodes.FirstOrDefaultAsync(p => p.Id == promoCodeId);
    }

    public async Task AddPromoCodeAsync(PromoCode? promoCode)
    {
        if (promoCode != null)
        {
            await _dbContext.PromoCodes.AddAsync(promoCode);
        }
        
    }

    public async Task UpdatePromoCodeAsync(PromoCode promoCode)
    {
         _dbContext.PromoCodes.Update(promoCode);
         await _dbContext.SaveChangesAsync();
    }
}