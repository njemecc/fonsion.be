namespace Fonsion.be.Contracts.Rooms;

public record RoomDto( Guid Id, string Name, int Capacity, decimal PricePerNight, string Description, List<string> ImageUrls);
