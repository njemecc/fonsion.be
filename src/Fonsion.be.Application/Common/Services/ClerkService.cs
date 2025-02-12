using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Fonsion.be.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

public class ClerkService : IClerkService
{
    private readonly HttpClient _httpClient;
    private readonly string _clerkApiKey;

    public ClerkService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _clerkApiKey = configuration["Clerk:ApiKey"];
    }

    public async Task<bool> UpdateUserMetadataAsync(string clerkUserId, Guid backendUserId)
    {
        var requestUrl = $"https://api.clerk.dev/v1/users/{clerkUserId}/metadata";

        var requestBody = new
        {
            public_metadata = new
            {
                userId = backendUserId.ToString() 
            }
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_clerkApiKey}");

        var response = await _httpClient.PatchAsync(requestUrl, jsonContent);

        return response.IsSuccessStatusCode;
    }
}