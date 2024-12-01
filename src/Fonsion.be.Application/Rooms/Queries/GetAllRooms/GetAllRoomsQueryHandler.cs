using ErrorOr;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Application.Common.Mappers;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler: IRequestHandler<GetAllRoomsQuery,ErrorOr<List<CreateRoomResponse>>>
{
    private readonly IRoomsRepository _roomsRepository;

    public GetAllRoomsQueryHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<List<CreateRoomResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomsRepository.GetAllRoomsAsync();
        
        if (rooms == null)
        {
            return Error.NotFound("No rooms found.");
        }

        var roomDtos = rooms.Select(room => room.FromModelToResponse()).ToList();

        return roomDtos;
    }
}