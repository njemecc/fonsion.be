using Fonsion.be.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fonsion.be.Api.Controllers.webhooks.clerk;

[ApiController]
[Route("api/webhooks/clerk")]
public class ClerkWebhookController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public ClerkWebhookController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> HandleWebhook()
    {
        var webhookSecret = _configuration["Clerk:WebhookSecret"];
        if (string.IsNullOrEmpty(webhookSecret))
        {
            return BadRequest("Webhook secret is not configured.");
        }

        var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
        var svixId = Request.Headers["svix-id"].ToString();
        var svixTimestamp = Request.Headers["svix-timestamp"].ToString();
        var svixSignature = Request.Headers["svix-signature"].ToString();

        if (string.IsNullOrEmpty(svixId) || string.IsNullOrEmpty(svixTimestamp) || string.IsNullOrEmpty(svixSignature))
        {
            return BadRequest("Missing webhook headers.");
        }

        // Log for debugging
        Console.WriteLine($"Request Body: {requestBody}");
        Console.WriteLine($"Svix ID: {svixId}");
        Console.WriteLine($"Svix Timestamp: {svixTimestamp}");
        Console.WriteLine($"Svix Signature: {svixSignature}");

        // if (!VerifySignature(requestBody, svixId, svixTimestamp, svixSignature, webhookSecret))
        // {
        //     return Unauthorized("Invalid webhook signature.");
        // }

        var webhookEvent = JsonSerializer.Deserialize<ClerkWebhookEvent>(requestBody);
        if (webhookEvent == null)
        {
            return BadRequest("Invalid webhook payload.");
        }

        switch (webhookEvent.Type)
        {
            case "user.created":
                var email = webhookEvent.Data.EmailAddresses?.FirstOrDefault()?.EmailAddresss;
                var user = new User
                {
                    Email = email,
                    FirstName = webhookEvent.Data.FirstName,
                    LastName = webhookEvent.Data.LastName,
                    ClerkId = webhookEvent.Data.Id,
                    
                };
                await _userManager.CreateAsync(user);
                break;

            default:
                return Ok("Event type not handled.");
        }

        return Ok("Webhook processed successfully.");
    }

   
    private bool VerifySignature(string payload, string svixId, string svixTimestamp, string svixSignature, string secret)
    {
        try
        {
            // Prepare timestamped payload
            var timestampedPayload = $"{svixId}.{svixTimestamp}.{payload}";
            Console.WriteLine($"Timestamped Payload: {timestampedPayload}");

            // Decode secret from Base64 to byte array
            var secretBytes = Convert.FromBase64String(secret);

            // Compute HMAC SHA256 hash
            using var hmac = new HMACSHA256(secretBytes);
            var computedSignature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(timestampedPayload)));
            Console.WriteLine($"Computed Signature: {computedSignature}");

            // Extract v1 signature from received signatures
            var receivedSignature = svixSignature.Split(',')
                .FirstOrDefault(part => part.StartsWith("v1="))
                ?.Replace("v1=", "").Trim();
            if (string.IsNullOrEmpty(receivedSignature))
            {
                Console.WriteLine("No v1 signature found.");
                return false;
            }

            Console.WriteLine($"Received Signature: {receivedSignature}");

            // Secure comparison
            var isValid = CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(receivedSignature),
                Encoding.UTF8.GetBytes(computedSignature)
            );

            Console.WriteLine($"Signature Valid: {isValid}");
            return isValid;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in signature verification: {ex.Message}");
            return false;
        }
    }

}

public class ClerkWebhookEvent
{
    public string Type { get; set; }
    public ClerkUserData Data { get; set; }
    public EventAttributes EventAttributes { get; set; }
    public long Timestamp { get; set; }
    public string Object { get; set; }
}

public class ClerkUserData
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImageUrl { get; set; }
    public bool HasImage { get; set; }
    public bool Banned { get; set; }
    public bool Locked { get; set; }
    
    // Ensure EmailAddresses is defined as a list of EmailAddress objects
    public List<EmailAddress> EmailAddresses { get; set; } 
}

public class EmailAddress
{
    public string EmailAddresss { get; set; }
    public string Id { get; set; }
    public List<LinkedAccount> LinkedTo { get; set; }
    public string Object { get; set; }
    public bool Reserved { get; set; }
    public Verification Verification { get; set; }
}

public class LinkedAccount
{
    public string Id { get; set; }
    public string Type { get; set; }
}

public class Verification
{
    public string Status { get; set; }
    public string Strategy { get; set; }
    public int? Attempts { get; set; }
    public long? ExpireAt { get; set; }
}

public class EventAttributes
{
    public HttpRequest HttpRequest { get; set; }
}

public class HttpRequest
{
    public string ClientIp { get; set; }
    public string UserAgent { get; set; }
}

