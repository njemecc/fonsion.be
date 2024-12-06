namespace Fonsion.be.Contracts.Rooms;


    public record CreateRoomResponse( string Name, int Capacity, decimal PricePerNight, string Description, string ImageUrl);
