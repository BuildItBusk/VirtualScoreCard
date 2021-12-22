namespace GolfScoreUI.Data
{
    public class Scorecard
    {
        public Guid Id { get; } = Guid.NewGuid();

        public List<Player> Players { get; init; } = new List<Player>();

        public List<HoleScore> Scores { get; init; } = new List<HoleScore>();

        public int MaxStrokes { get; init; } = 8;

        public int NumberOfHoles { get; init; } = 9;

        public void SetScore(Guid playerId, int holeNumber, int numberOfStrokes)
        {
            var hole = Scores.Where(s => s.PlayerId == playerId && s.HoleNumber == holeNumber).First();
            hole.NumberOfStrokes = numberOfStrokes;
        }

        public int ScoreSum(Guid playerId) => Scores.Where(s => s.PlayerId == playerId).Sum(s => s.NumberOfStrokes);
    }
}
