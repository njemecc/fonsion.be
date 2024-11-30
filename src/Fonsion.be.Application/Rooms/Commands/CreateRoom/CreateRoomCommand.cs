using ErrorOr;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Rooms.Commands;

public record CreateRoomCommand(CreateRoomRequest CreateRoomRequest) : IRequest<ErrorOr<CreateRoomResponse>>;
