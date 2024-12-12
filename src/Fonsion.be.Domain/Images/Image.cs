namespace Fonsion.be.Domain.Images;

public class Image
{
    public Guid Id { get; set; }
    
    public string Url { get; set; }

    public Guid RoomId { get; set; }
}