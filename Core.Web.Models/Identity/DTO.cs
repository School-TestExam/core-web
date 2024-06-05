using System.Text.Json.Serialization;

namespace Core.Web.Models.Identity;

public class DTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("username")]
    public string Username {  get; set; }
    
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}