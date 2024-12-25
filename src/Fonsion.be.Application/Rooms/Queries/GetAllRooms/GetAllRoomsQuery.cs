using ErrorOr;
using Fonsion.be.Application.Common.Helpers;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Rooms.Queries.GetAllRooms;

public record GetAllRoomsQuery( QueryObject? QueryObject) : IRequest<ErrorOr<List<RoomDto>>>;