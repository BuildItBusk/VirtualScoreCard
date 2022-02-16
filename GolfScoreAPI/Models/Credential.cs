namespace GolfScoreAPI.Models;

public class Credential
{
    public Credential(Guid userId, string username, string password)
    {
        UserId = userId;
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }

    public Guid UserId { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}
