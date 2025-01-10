using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Reservations;
using Fonsion.be.Application.Common.Enums.Reservation;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.GuestCompanion;
using Fonsion.be.Domain.Reservations;
using Fonsion.be.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fonsion.be.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand,ErrorOr<CreateReservationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IReservationsRepository _reservationsRepository;

    private readonly IReservationService _reservationService;

    private readonly IRoomsRepository _roomRepository;

    private readonly IPromoCodeRepository _promoCodeRepository;

    private IGuestCompanionRepository _guestCompanionRepository;

    private readonly UserManager<User> _userManager;

    public CreateReservationCommandHandler(IUnitOfWork unitOfWork, IReservationsRepository reservationsRepository, IReservationService reservationService, IRoomsRepository roomRepository, IPromoCodeRepository promoCodeRepository, IGuestCompanionRepository guestCompanionRepository, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _reservationsRepository = reservationsRepository;
        _reservationService = reservationService;
        _roomRepository = roomRepository;
        _promoCodeRepository = promoCodeRepository;
        _guestCompanionRepository = guestCompanionRepository;
        _userManager = userManager;
    }

    public async Task<ErrorOr<CreateReservationResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var existingReservations =
            await _reservationsRepository.GetReservationsForRoomAsync(request.CreateReservationRequest.RoomId);
        
        bool isRoomAvailable = !existingReservations.Any(reservation =>
            (request.CreateReservationRequest.FromDate >= reservation.FromDate && request.CreateReservationRequest.FromDate <= reservation.ToDate) ||
            (request.CreateReservationRequest.ToDate >= reservation.FromDate && request.CreateReservationRequest.ToDate <= reservation.ToDate) ||
            (request.CreateReservationRequest.FromDate <= reservation.FromDate && request.CreateReservationRequest.ToDate >= reservation.ToDate)
        );
        
        if (!isRoomAvailable)
        {
            return Error.Conflict("Room is not available for this period of time.");
        }

        var room = await _roomRepository.GetRoomById(request.CreateReservationRequest.RoomId);
        

        if (room == null)
        {
            return Error.Conflict("Room does not exists!");
        }

        if (room.Capacity < request.CreateReservationRequest.GuestCompanions.Count)
        {
            return Error.Conflict($"Maximum number of guests for this room is {room.Capacity}");
        }

        var user = await _userManager.FindByIdAsync(request.CreateReservationRequest.UserId.ToString());

        if (user == null)
        {
            return Error.Conflict("That user does not exists");
        }

       
        var promoCode = await _promoCodeRepository.GetPromoCodeByIdAsync(request.CreateReservationRequest.PromoCodeId);

        if (promoCode == null && request.CreateReservationRequest.PromoCodeId != null)
        {
            return Error.Conflict("Promo code is not valid");
        }


        var newReservation = new Reservation
        {
            Status = ReservationStatus.Active,
            UserId = request.CreateReservationRequest.UserId,
            RoomId = request.CreateReservationRequest.RoomId,
            FromDate = request.CreateReservationRequest.FromDate,
            ToDate = request.CreateReservationRequest.ToDate,
            TotalPrice = _reservationService.CalculatePrice(request.CreateReservationRequest.FromDate,
                request.CreateReservationRequest.ToDate, room.PricePerNight, promoCode != null && promoCode.Active ? promoCode.Value : 0)
        };

        await _reservationsRepository.AddReservationAsync(newReservation);

        //set promocode to not active
        if (promoCode != null)
        {
            promoCode.Active = false;
            await _promoCodeRepository.UpdatePromoCodeAsync(promoCode);
        }
        

        foreach (var guest in request.CreateReservationRequest.GuestCompanions.Where(g => g != null))
        {
            var companion = new GuestCompanion
            {
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                ReservationId = newReservation.Id
            };

            await _guestCompanionRepository.AddGuestCompanionAsync(companion);
        }

        var newPromoCode = _reservationService.GeneratePromoCode();
        await _promoCodeRepository.AddPromoCodeAsync(newPromoCode);

        newReservation.PromoCode = newPromoCode;

        await _unitOfWork.CommitChangesAsync();

        return new CreateReservationResponse(newPromoCode);
        
    }
}