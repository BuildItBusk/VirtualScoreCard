namespace GolfScoreAPI.Models;

public class Scorecard
{
    public Scorecard(UserProfile user, int numberOfHoles)
    {
        if (numberOfHoles < 1)
            throw new ArgumentOutOfRangeException(nameof(numberOfHoles));

        UserId = user.Id;
        NumberOfHoles = numberOfHoles;
        PlayerNames.Add(user.Username);
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; init; }

    public int NumberOfHoles { get; init; }

    public List<string> PlayerNames { get; init; } = new List<string>();

    public List<Score> Scores { get; init; } = new List<Score>();

    public void AddPlayer(string name)
    {
        if (PlayerNames.Contains(name))
            throw new ArgumentException("Player already exists.");

        if (PlayerNames.Count >= 4)
            throw new InvalidOperationException("A maximum of four players has already been added to the scorecard.");

        PlayerNames.Add(name);
    }

    public void AssignScore(string name, int hole, int strokes)
    {
        if (hole > NumberOfHoles)
            throw new ArgumentOutOfRangeException("Cannot assign score to a hole greater than the total number of holes.");

        var current = Scores.FirstOrDefault(s => s.PlayerName == name && s.HoleNumber == hole);

        if (current is not null)
            current = new Score(name, hole, strokes);
        else
            Scores.Add(new Score(name, hole, strokes));
        
    }

    public int ScoreSum(string playerName) => Scores
                                             .Where(s => s.PlayerName == playerName)
                                             .Sum(s => s.Strokes);
}
