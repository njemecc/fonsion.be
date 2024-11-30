using ErrorOr;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Application.Common.Mappers;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler:IRequestHandler<CreateRoomCommand,ErrorOr<CreateRoomResponse>>
{

    private readonly IRoomsRepository _roomsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork)
    {
        _roomsRepository = roomsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateRoomResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {

        var room = request.CreateRoomRequest.FromRequestToModel();

        var createRoomResponse = room.FromModelToResponse();
        
        await _roomsRepository.AddRoomAsync(room);
        //unit of work pattern:
        await _unitOfWork.CommitChangesAsync();

        return createRoomResponse;

    }
}