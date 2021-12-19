using GolfScoreUI.Extensions;

namespace GolfScoreUI.Data
{
    public class Scorecard
    {
        public Scorecard(int numberOfHoles, int maxStrokes, List<Player> players)
        {
            NumberOfHoles = numberOfHoles;
            MaxStrokes = maxStrokes;
            Players = players;

            foreach (var player in players)
            {
                for (int i = 1; i <= NumberOfHoles; i++)
                    Scores.Add(new HoleScore { HoleNumber = i, PlayerId = player.Id });
            }            
        }

        public Guid Id { get; } = Guid.NewGuid();

        public List<Player> Players { get; } = new List<Player>();

        public List<HoleScore> Scores { get; init; } = new List<HoleScore>();

        public int MaxStrokes { get; set; } = 8;

        public int NumberOfHoles { get; } = 9;

        public int ScoreSum(Guid playerId) => Scores.Where(s => s.PlayerId == playerId).Sum(s => s.NumberOfStrokes);
    }
}
