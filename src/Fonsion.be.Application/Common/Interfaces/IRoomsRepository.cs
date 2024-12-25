using ErrorOr;
using Fonsion.be.Application.Common.Helpers;
using Fonsion.be.Domain.Entities;

namespace Fonsion.be.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task AddRoomAsync(Room room);

    Task<List<Room>> GetAllRoomsAsync(QueryObject? queryObject);

    Task<Room?> GetRoomById(Guid roomId);
}