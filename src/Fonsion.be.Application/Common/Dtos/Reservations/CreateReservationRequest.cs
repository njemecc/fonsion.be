using Fonsion.be.Application.Common.Dtos.GuestCompanions;
using Fonsion.be.Domain.GuestCompanion;
using Fonsion.be.Domain.PromoCodes;

namespace Fonsion.be.Application.Common.Dtos.Reservations;

public record CreateReservationRequest(Guid UserId, Guid RoomId ,DateTime FromDate, DateTime ToDate, List<GuestCompanionDto?> GuestCompanions, Guid? PromoCodeId );