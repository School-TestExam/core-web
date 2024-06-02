using System.ComponentModel;

namespace Core.Web.Models.Identity.Requests;

public class Create
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    
    [DefaultValue("SYSTEM")]
    public string CreatedBy { get; set; } = "SYSTEM";
}