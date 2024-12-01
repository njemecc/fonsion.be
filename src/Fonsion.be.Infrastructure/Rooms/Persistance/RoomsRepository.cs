using Fonsion.be.Application.Common.Interfaces;
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

       //ovo mi ne treba zbog unit of work patterna
     //  await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Room>> GetAllRoomsAsync()
    {
     return await _dbContext.Rooms.ToListAsync();
    }
}