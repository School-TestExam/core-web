using System.Text;
using System.Text.Json;
using Core.Web.Models.Identity.Requests;
using DTO = Core.Web.Models.Identity.DTO;

namespace Core.Web.Client.Clients;

public interface IIdentityClient
{
    public Task<DTO?> Create(Create request);
    public Task Delete(Guid id);
    public Task<DTO?> Get(Guid id);
    public Task<DTO?> Update(Guid id, Update request);
}

public class IdentityClient : IIdentityClient
{
    private readonly HttpClient _client;

    public IdentityClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<DTO?> Create(Create request)
    {
        var content = JsonSerializer.Serialize(request);
        var payload = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{_client.BaseAddress}/identity", payload);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"{response.ReasonPhrase}");
        }
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<DTO>(responseContent);
        return dto!;
    }
    public async Task Delete(Guid id)
    {
        var response = await _client.DeleteAsync($"{_client.BaseAddress}/identity/{id}");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"{response.ReasonPhrase}");
        }
    }
    
    public async Task<DTO?> Get(Guid id)
    {
        var response = await _client.GetAsync($"{_client.BaseAddress}/identity/{id}");
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"{response.ReasonPhrase}");
        }
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<DTO>(responseContent);
        return dto!;
    }

    public async Task<DTO?> Update(Guid id, Update request)
    {
        var content = JsonSerializer.Serialize(request);
        var payload = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"{_client.BaseAddress}/identity/{id}", payload);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"{response.ReasonPhrase}");
        }
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<DTO>(responseContent);
        return dto!;
    }
}