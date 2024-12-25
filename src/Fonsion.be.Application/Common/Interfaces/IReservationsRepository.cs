using Fonsion.be.Domain.Reservations;

namespace Fonsion.be.Application.Common.Interfaces;

public interface IReservationsRepository
{
    Task AddReservationAsync(Reservation reservation);

    Task<IEnumerable<Reservation>> GetReservationsForRoomAsync(Guid roomId);
    
    Task<List<string>> GetReservedDatesByRoomIdAsync(Guid roomId);


}