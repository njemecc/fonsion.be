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

    public async Task<List<string>> GetReservedDatesByRoomIdAsync(Guid roomId)
    {
        var reservations = await _dbContext.Reservations
            .Where(r => r.RoomId == roomId)
            .Select(r => new { r.FromDate, r.ToDate })
            .ToListAsync();

        // Generisanje lista datuma za svaku rezervaciju
        var occupiedDates = new List<string>();
        foreach (var reservation in reservations)
        {
            for (var date = reservation.FromDate.Date; date <= reservation.ToDate.Date; date = date.AddDays(1))
            {
                occupiedDates.Add(date.ToString("yyyy-MM-dd")); // ISO 8601 format
            }
        }

        return occupiedDates;
    }
}