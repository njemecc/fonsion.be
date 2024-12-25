using Fonsion.be.Contracts.Rooms;
using Fonsion.be.Domain.Entities;

namespace Fonsion.be.Application.Common.Mappers;

public static class RoomsMapper
{

    public static Room FromRequestToModel(this CreateRoomRequest createRoomRequest)
    {
        return new Room()
        {
            Id = Guid.NewGuid(),
            Name = createRoomRequest.Name,
            Description = createRoomRequest.Description,
            Capacity = createRoomRequest.Capacity,
            PricePerNight = createRoomRequest.pricePerNight

        };
    }

    public static CreateRoomResponse FromModelToResponse(this Room room)
    {
        return new CreateRoomResponse(room.Name, room.Capacity, room.PricePerNight, room.Description);
    }

    public static RoomDto ToDto(this Room room)
    {
        return new RoomDto(room.Id, room.Name, room.Capacity, room.PricePerNight, room.Description,room.Images.Select(i => i.Url).ToList());
    }
    
}