using GolfScoreUI.Extensions;

namespace GolfScoreUI.Data
{
    public class Scorecard
    {
        public Scorecard(List<Player> players)
        {
            Players = players;
            NumberOfHoles = 18;
            Score = new int[players.Count, NumberOfHoles];            
        }

        public Guid Id { get; } = Guid.NewGuid();

        public List<Player> Players { get; }

        public int[,] Score { get; }

        public int NumberOfHoles { get; }

        public int[] ScoreSum => Score.Sum(1);
    }
}
