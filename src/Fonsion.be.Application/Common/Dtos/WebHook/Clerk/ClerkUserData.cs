using System.Text.Json.Serialization;

public class ClerkUserData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("email_addresses")]
    public List<EmailAddress> EmailAddresses { get; set; }
}

public class EmailAddress
{
    [JsonPropertyName("email_address")]
    public string Email { get; set; }
}