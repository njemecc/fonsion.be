using ErrorOr;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Rooms.Queries.GetAllRooms;

public record GetAllRoomsQuery() : IRequest<ErrorOr<List<CreateRoomResponse>>>;