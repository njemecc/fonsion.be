using System.Net;
using System.Text.Json;
using System.IO;
using Fonsion.be.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Svix;
using Svix.Exceptions;
using Fonsion.be.Application.Common.Dtos.WebHook.Clerk;
using System.Linq;
using Fonsion.be.Application.Common.Dtos.Users;
using Fonsion.be.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Fonsion.be.Api.Controllers.Webhooks.Clerk
{
    [ApiController]
    [Route("api/webhooks/clerk")]
    public class ClerkWebhookController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IClerkService _clerkService;

        public ClerkWebhookController(UserManager<User> userManager, IConfiguration configuration, IClerkService clerkService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _clerkService = clerkService;
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
            
            WebHeaderCollection webHeaders = new WebHeaderCollection();
            foreach (var header in Request.Headers)
            {
                webHeaders[header.Key] = header.Value.ToString();
            }

            try
            {
                var webhook = new Webhook(webhookSecret);
                webhook.Verify(requestBody, webHeaders); // Verifikacija bez vraćanja payload-a

                Console.WriteLine($"RequestBody: {requestBody}");

                var webhookEvent = JsonSerializer.Deserialize<ClerkWebhookEvent>(requestBody);
                
                if (webhookEvent?.Data == null)
                {
                    return BadRequest("Invalid webhook payload or missing data.");
                }

                if (webhookEvent.Type == "user.created")
                {
                    var email = webhookEvent.Data?.EmailAddresses.FirstOrDefault()?.Email;


                    if (string.IsNullOrEmpty(email))
                    {
                        return BadRequest("Email address is missing.");
                    }

                    var user = new User
                    {
                        Email = email,
                        FirstName = webhookEvent.Data.FirstName,
                        LastName = webhookEvent.Data.LastName,
                        ClerkId = webhookEvent.Data.Id,
                        UserName = email
                    };

                    var result = await _userManager.CreateAsync(user);

                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "Guest");
                        
                        var updateSuccess = await _clerkService.UpdateUserMetadataAsync(user.ClerkId, new Guid(user.Id));
                        
                        if (!updateSuccess)
                        {
                            return StatusCode(500, "Failed to update Clerk metadata.");
                        }
                        
                        if (!roleResult.Succeeded)
                        {
                            return BadRequest(
                                $"Role assignment failed: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }

                        return Ok("User successfully created and assigned role.");

                    }
                }
                else
                {
                    return Ok("Event type not handled.");
                }

                return Ok("Webhook processed successfully.");
            }
            catch (WebhookVerificationException)
            {
                return Unauthorized("Invalid webhook signature.");
            }
            catch (Exception ex)
            {
                // Zabilježiti grešku
                Console.WriteLine($"Internal server error : {ex.ToString()}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
