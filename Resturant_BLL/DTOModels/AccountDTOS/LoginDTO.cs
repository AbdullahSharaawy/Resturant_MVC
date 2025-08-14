using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

public class LoginDTO
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
    public IList<AuthenticationScheme>? ExternalLogins { get; set; }
}