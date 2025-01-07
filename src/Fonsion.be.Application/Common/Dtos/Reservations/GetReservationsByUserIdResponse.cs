namespace Fonsion.be.Application.Common.Dtos.Reservations;

public record GetReservationsByUserIdResponse(string RoomName, IEnumerable<string> Images, DateTime FromDate, DateTime ToDate, decimal TotalPrice);