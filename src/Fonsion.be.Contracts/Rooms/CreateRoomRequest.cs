namespace Fonsion.be.Contracts.Rooms;

public record CreateRoomRequest( string Name, int Capacity, decimal pricePerNight, string Description, string ImageUrl);