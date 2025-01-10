using Fonsion.be.Application.Common.Dtos.Reservations;
using Fonsion.be.Application.Common.Enums.Reservation;
using Fonsion.be.Domain.Reservations;

namespace Fonsion.be.Application.Common.Mappers;

public static class ReservationMapper
{
    public static GetReservationsByUserIdResponse FromModelToGetReservationsByUserIdResponse(this Reservation reservation)
    {
        return new GetReservationsByUserIdResponse(reservation.Room.Name, reservation.Room.Images.Select(i => i.Url), reservation.FromDate, reservation.ToDate, reservation.TotalPrice,reservation.Id,reservation.Status = reservation.Status);
    }
}