namespace GolfScoreAPI.Models;

public class Score
{
    public Score(string playerName, int holeNumber, int strokes)
    {
        if (holeNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(holeNumber));

        if (strokes <= 0)
            throw new ArgumentOutOfRangeException(nameof(strokes));

        if (string.IsNullOrWhiteSpace(playerName))
            throw new ArgumentException("Player name cannot be empty.");

        PlayerName = playerName;
        HoleNumber = holeNumber;
        Strokes = strokes;
    }

    public Guid ScorecardId { get; init; }
    public string PlayerName { get; init; }
    public int HoleNumber { get; init; }
    public int Strokes { get; init; }

}
