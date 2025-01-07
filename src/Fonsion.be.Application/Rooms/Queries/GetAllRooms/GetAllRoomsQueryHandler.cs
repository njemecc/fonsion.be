using ErrorOr;
using Fonsion.be.Application.Common.Helpers;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Application.Common.Mappers;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Rooms.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler: IRequestHandler<GetAllRoomsQuery,ErrorOr<PaginatedResult<RoomDto>>>
{
    private readonly IRoomsRepository _roomsRepository;

    public GetAllRoomsQueryHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task<ErrorOr<PaginatedResult<RoomDto>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var paginatedResultRooms = await _roomsRepository.GetAllRoomsAsync(request.QueryObject);
        
        if (paginatedResultRooms.Items == null)
        {
            return Error.NotFound("No rooms found.");
        }

        //var roomDtos = rooms.Select(room => room.ToDto()).ToList();
        
        var paginatedResult = new PaginatedResult<RoomDto>
        {
            TotalCount = paginatedResultRooms.TotalCount,
            Items = paginatedResultRooms.Items.Select(r => r.ToDto()).ToList()
        };

        return paginatedResult;
    }
}