using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Reservations;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Application.Common.Mappers;
using MediatR;

namespace Fonsion.be.Application.Reservations.Queries.GetReservationsByUserId;

public class GetReservationsByUserIdQueryHandler: IRequestHandler<GetReservationsByUserIdQuery,ErrorOr<List<GetReservationsByUserIdResponse>>>
{
    private readonly IReservationsRepository _reservationsRepository;

    public GetReservationsByUserIdQueryHandler(IReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
    }

    public async Task<ErrorOr<List<GetReservationsByUserIdResponse>>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationsRepository.GetReservationsForUserAsync(request.userId);

        var reservationDtos = reservations.Select(r => r.FromModelToGetReservationsByUserIdResponse()).ToList();

        return reservationDtos;
    }
}