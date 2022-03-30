using System.ComponentModel.DataAnnotations;

namespace eCommerce.ViewModels;

public class AkunLoginViewModel
{
    public AkunLoginViewModel()
    {
        Username = string.Empty;
        Password = string.Empty;
    }
    public AkunLoginViewModel(string username, string password)
    {
        Username = username;
        Password = password;
    }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}