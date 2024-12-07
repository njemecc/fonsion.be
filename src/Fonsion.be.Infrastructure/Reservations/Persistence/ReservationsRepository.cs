using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.Reservations;
using Fonsion.be.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fonsion.be.Infrastructure.Reservations.Persistence;

public class ReservationsRepository : IReservationsRepository
{
    private readonly FonsionDbContext _dbContext;

    public ReservationsRepository(FonsionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
         await _dbContext.Reservations.AddAsync(reservation);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsForRoomAsync(Guid roomId)
    {
        return await _dbContext.Reservations.Where(r => r.RoomId == roomId && r.ToDate >= DateTime.UtcNow)
            .ToListAsync();
    }
}