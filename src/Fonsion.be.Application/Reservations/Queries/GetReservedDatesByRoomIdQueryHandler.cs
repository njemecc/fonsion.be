using ErrorOr;
using Fonsion.be.Application.Common.Interfaces;
using MediatR;

namespace Fonsion.be.Application.Reservations.Queries;

public class GetReservedDatesByRoomIdQueryHandler : IRequestHandler<GetReservedDatesByRoomIdQuery,ErrorOr<List<string>>>
{
    
    private readonly IReservationsRepository _reservationsRepository;

    public GetReservedDatesByRoomIdQueryHandler(IReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
    }


    public async Task<ErrorOr<List<string>>> Handle(GetReservedDatesByRoomIdQuery request, CancellationToken cancellationToken)
    {
        return await _reservationsRepository.GetReservedDatesByRoomIdAsync(request.roomId);
    }
}
