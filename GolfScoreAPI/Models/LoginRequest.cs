using System.ComponentModel.DataAnnotations;

namespace GolfScoreAPI.Models;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
