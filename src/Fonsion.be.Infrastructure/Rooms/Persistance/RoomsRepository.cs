using Fonsion.be.Application.Common.Helpers;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Contracts.Rooms;
using Fonsion.be.Domain.Entities;
using Fonsion.be.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fonsion.be.Infrastructure.Rooms.Persistance;

public class RoomsRepository : IRoomsRepository
{
    private readonly FonsionDbContext _dbContext;

    public RoomsRepository(FonsionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRoomAsync(Room room)
    {
       await _dbContext.Rooms.AddAsync(room);

    
    }

    public async Task<PaginatedResult<Room>> GetAllRoomsAsync(QueryObject? queryObject)
    {
        var fromDate = queryObject?.FromDate;
        var toDate = queryObject?.ToDate;
        var page = queryObject?.Page ?? 0;  
        var pageSize = queryObject?.PageSize ?? 5;  

        var query = _dbContext.Rooms
            .Include(room => room.Images)
            .Include(room => room.Reservations)
            .AsQueryable();

        if (fromDate.HasValue && toDate.HasValue)
        {
            query = query.Where(room => !room.Reservations.Any(reservation =>
                reservation.ToDate >= fromDate && reservation.FromDate <= toDate));
        }

        // Paginacija
        var rooms = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var paginatedResult = new PaginatedResult<Room>
        {
            Items = rooms,
            TotalCount = query.Count()
        };

        return paginatedResult;
    }



    public async Task<Room?> GetRoomById(Guid roomId)
    {
        return await _dbContext.Rooms.Include(room => room.Images).FirstOrDefaultAsync(r => r.Id == roomId);
    }
}