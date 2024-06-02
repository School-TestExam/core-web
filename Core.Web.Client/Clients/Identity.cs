using System.Text;
using System.Text.Json;
using Core.Web.Models.Identity.Requests;
using DTO = Core.Web.Models.Identity.DTO;

namespace Core.Web.Client.Clients;

public interface IIdentityClient
{
    public Task<DTO> CreateIdentity(Create request);
}

public class IdentityClient : IIdentityClient
{
    private readonly HttpClient _client;

    public IdentityClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<DTO> CreateIdentity(Create request)
    {
        var content = JsonSerializer.Serialize(request);
        var payload = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{_client.BaseAddress}/identity", payload);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create identity");
        }
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<DTO>(responseContent);
        return dto!;
    }
}