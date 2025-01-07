using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Reservations;
using MediatR;

namespace Fonsion.be.Application.Reservations.Queries.GetReservationsByUserId;

public record GetReservationsByUserIdQuery(Guid userId) : IRequest<ErrorOr<List<GetReservationsByUserIdResponse>>>;