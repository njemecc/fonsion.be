using ErrorOr;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Application.Common.Mappers;
using Fonsion.be.Contracts.Rooms;
using Fonsion.be.Domain.Images;
using MediatR;

namespace Fonsion.be.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler:IRequestHandler<CreateRoomCommand,ErrorOr<CreateRoomResponse>>
{

    private readonly IRoomsRepository _roomsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImagesRepository _imagesRepository;

    public CreateRoomCommandHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork, IImagesRepository imagesRepository)
    {
        _roomsRepository = roomsRepository;
        _unitOfWork = unitOfWork;
        _imagesRepository = imagesRepository;
    }

    public async Task<ErrorOr<CreateRoomResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {

        var room = request.CreateRoomRequest.FromRequestToModel();

        var createRoomResponse = room.FromModelToResponse();
        
        await _roomsRepository.AddRoomAsync(room);
        
        foreach (var imageUrl in request.CreateRoomRequest.ImageUrls.Where(iurl => iurl != null))
        {
            var Image = new Image
            {
                Url = imageUrl,
                RoomId = room.Id
            };

            await _imagesRepository.AddImageAsync(Image);
        }
        
        //unit of work pattern:
        await _unitOfWork.CommitChangesAsync();

        return createRoomResponse;

    }
}