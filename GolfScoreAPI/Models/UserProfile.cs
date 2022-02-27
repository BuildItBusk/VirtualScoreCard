namespace GolfScoreAPI.Models;

public class UserProfile
{
    public UserProfile(string username, string email)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username));

        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));

        Username = username.Trim();
        Email = email.Trim();
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Username { get; init; }
    public string Email { get; private set; } = "";
    public DateTime Created { get; init; }
}
