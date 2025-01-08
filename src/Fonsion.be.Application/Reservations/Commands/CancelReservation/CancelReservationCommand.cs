using ErrorOr;
using MediatR;

namespace Fonsion.be.Application.Reservations.Commands.CancelReservation;

public record CancelReservationCommand(Guid ReservationId) : IRequest<ErrorOr<string>>;
    