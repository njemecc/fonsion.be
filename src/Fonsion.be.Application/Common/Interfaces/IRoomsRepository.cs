using Fonsion.be.Domain.Entities;

namespace Fonsion.be.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task AddRoomAsync(Room room);
}