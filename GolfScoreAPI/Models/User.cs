namespace GolfScoreAPI.Models;

public class User
{
    public string UserName { get; set; }
    public Guid Id { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
}
