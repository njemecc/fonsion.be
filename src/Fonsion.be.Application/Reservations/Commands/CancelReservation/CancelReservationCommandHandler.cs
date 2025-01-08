using ErrorOr;
using Fonsion.be.Application.Common.Interfaces;
using MediatR;

namespace Fonsion.be.Application.Reservations.Commands.CancelReservation;

public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand,ErrorOr<string>>
{
    
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IPromoCodeRepository _promoCodeRepository;

    public CancelReservationCommandHandler(IReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
    }

    public async Task<ErrorOr<string>> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation =  await _reservationsRepository.CancelReservationAsync(request.ReservationId);
        
        if (reservation.Errors?.Count > 0)
        {
            return reservation.Errors.ToErrorOr<string>();
        }

        var promoCode = reservation.Value.PromoCode;

        promoCode.Active = false;
        
        await _promoCodeRepository.UpdatePromoCodeAsync(promoCode);
        
        return "Reservation Canceled Successfully";
        
        
    }
}