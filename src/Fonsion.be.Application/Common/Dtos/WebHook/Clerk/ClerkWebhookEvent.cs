namespace Fonsion.be.Application.Common.Dtos.WebHook.Clerk;

using System.Text.Json.Serialization;

public class ClerkWebhookEvent
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("data")]
    public ClerkUserData Data { get; set; }

    [JsonPropertyName("event_attributes")]
    public EventAttributes EventAttributes { get; set; }

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("object")]
    public string Object { get; set; }
}

