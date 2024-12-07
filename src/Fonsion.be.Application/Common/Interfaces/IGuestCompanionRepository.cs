using Fonsion.be.Domain.GuestCompanion;

namespace Fonsion.be.Application.Common.Interfaces;

public interface IGuestCompanionRepository
{
    Task AddGuestCompanionAsync(GuestCompanion guestCompanion);
}