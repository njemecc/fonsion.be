using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Reservations;
using MediatR;

namespace Fonsion.be.Application.Reservations.Commands.CreateReservation;

public record CreateReservationCommand(CreateReservationRequest CreateReservationRequest) : IRequest<ErrorOr<CreateReservationResponse>>;