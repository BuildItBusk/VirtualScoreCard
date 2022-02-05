namespace GolfScoreAPI.Models;

public class UserProfile
{
    public UserProfile(string userName, string password)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string UserName { get; set; }
    public string Password { get; set; }
}
