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
            Score = new int[Players.Count, NumberOfHoles];
        }

        public Guid Id { get; } = Guid.NewGuid();

        public List<Player> Players { get; } = new List<Player>();

        public int[,] Score { get; private set; } = new int[0,0];

        public int MaxStrokes { get; set; } = 8;

        public int NumberOfHoles { get; } = 9;

        public int[] ScoreSum => Score.Sum(1);
    }
}
