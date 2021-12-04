using GolfScoreUI.Extensions;

namespace GolfScoreUI.Data
{
    public class Scorecard
    {
        public Guid Id { get; } = Guid.NewGuid();

        public List<Player> Players { get; } = new List<Player>();

        public int[,] Score { get; private set; } = new int[0,0];

        public int MaxStrokes { get; set; } = 8;

        public int NumberOfHoles { get; } = 9;

        public int[] ScoreSum => Score.Sum(1);

        public void AddPlayer(string name)
        {
            Players.Add(new Player { Name = name });
            Score = new int[Players.Count, NumberOfHoles];
        }
    }
}
