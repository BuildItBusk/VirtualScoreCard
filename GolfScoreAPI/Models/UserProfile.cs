namespace GolfScoreAPI.Models;

public class UserProfile
{
    public UserProfile(string username, string email)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Username { get; init; }
    public string Email { get; private set; } = "";
    public DateTime Created { get; init; }
}
