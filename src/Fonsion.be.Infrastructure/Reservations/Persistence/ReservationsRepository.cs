﻿using ErrorOr;
using Fonsion.be.Application.Common.Enums.Reservation;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.Reservations;
using Fonsion.be.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fonsion.be.Infrastructure.Reservations.Persistence;

public class ReservationsRepository : IReservationsRepository
{
    private readonly FonsionDbContext _dbContext;

    public ReservationsRepository(FonsionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        await _dbContext.Reservations.AddAsync(reservation);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsForRoomAsync(Guid roomId)
    {
        return await _dbContext.Reservations.Where(r => r.RoomId == roomId && r.ToDate >= DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<List<string>> GetReservedDatesByRoomIdAsync(Guid roomId)
    {
        var reservations = await _dbContext.Reservations
            .Where(r => r.RoomId == roomId)
            .Select(r => new { r.FromDate, r.ToDate })
            .ToListAsync();

        // Generisanje lista datuma za svaku rezervaciju
        var occupiedDates = new List<string>();
        foreach (var reservation in reservations)
        {
            for (var date = reservation.FromDate.Date; date <= reservation.ToDate.Date; date = date.AddDays(1))
            {
                occupiedDates.Add(date.ToString("yyyy-MM-dd")); // ISO 8601 format
            }
        }

        return occupiedDates;
    }

    public async Task<List<Reservation>> GetReservationsForUserAsync(Guid userId)
    {
        return await _dbContext.Reservations
            .Where(r => r.UserId == userId)
            .Include(r => r.Room)
            .ThenInclude(room => room.Images)
            .ToListAsync();
    }

    public async Task<ErrorOr<Reservation>> CancelReservationAsync(Guid reservationId)
    {
        var reservation = await _dbContext.Reservations
            .Include(r => r.PromoCode) 
            .FirstOrDefaultAsync(r => r.Id == reservationId);
        if (reservation == null)
        {
            return Error.NotFound("Reservation not found.");
        }

        if (reservation.Status == ReservationStatus.Canceled)
        {
            return Error.Conflict("Reservation is already canceled.");
        }

       
        var daysUntilStart = (reservation.FromDate - DateTime.UtcNow).TotalDays;
        
        if (daysUntilStart < 5)
        {
            return Error.Validation("Cannot cancel reservation less than 5 days before start date.");
        }
        
        reservation.Status = ReservationStatus.Canceled;
        await _dbContext.SaveChangesAsync();

        return reservation;
    }

}
