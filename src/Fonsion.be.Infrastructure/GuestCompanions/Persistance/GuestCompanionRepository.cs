using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.GuestCompanion;
using Fonsion.be.Infrastructure.Common.Persistence;

namespace Fonsion.be.Infrastructure.GuestCompanions.Persistance;

public class GuestCompanionRepository : IGuestCompanionRepository
{
    private readonly FonsionDbContext _dbContext;

    public GuestCompanionRepository(FonsionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddGuestCompanionAsync(GuestCompanion guestCompanion)
    {
        await _dbContext.GuestCompanions.AddAsync(guestCompanion);
    }
}