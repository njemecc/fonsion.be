using Fonsion.be.Application.Common.Enums.Reservation;

namespace Fonsion.be.Application.Common.Dtos.Reservations;

public record GetReservationsByUserIdResponse(string RoomName, IEnumerable<string> Images, DateTime FromDate, DateTime ToDate, decimal TotalPrice, Guid ReservationId, ReservationStatus Status );