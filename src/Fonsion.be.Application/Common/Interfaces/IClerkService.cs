namespace Fonsion.be.Application.Common.Interfaces;

public interface IClerkService
{
    Task<bool> UpdateUserMetadataAsync(string clerkUserId, Guid backendUserId);
}