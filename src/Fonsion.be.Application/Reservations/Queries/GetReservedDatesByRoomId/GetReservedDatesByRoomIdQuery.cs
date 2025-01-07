using ErrorOr;
using MediatR;

namespace Fonsion.be.Application.Reservations.Queries;

public record GetReservedDatesByRoomIdQuery(Guid roomId) : IRequest<ErrorOr<List<string>>>;
