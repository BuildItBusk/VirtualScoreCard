namespace GolfScoreUI.Data
{
    public class Scorecard
    {
        public Scorecard(Guid id, int numberOfHoles, int maxStrokes, List<Player> players, List<HoleScore> scores) : this(numberOfHoles, maxStrokes, players)
        {
            // This method is the 'Constructor initialiser'. It allows setting the Id prior to running the constructor.
            Id = id;          
            Scores = scores ?? throw new ArgumentNullException(nameof(scores));
        }

        public Scorecard(int numberOfHoles, int maxStrokes, List<Player> players)
        {
            Players = players ?? throw new ArgumentNullException(nameof(players));
            NumberOfHoles = numberOfHoles > 0 ? numberOfHoles : throw new ArgumentOutOfRangeException(nameof(numberOfHoles));
            MaxStrokes = maxStrokes > 0 ? maxStrokes : throw new ArgumentOutOfRangeException(nameof(maxStrokes));

            // We don't need to add score, if they have already been given in the Constructor Initialiser.
            if (Scores.Count > 0)
                return;

            foreach (var player in Players)
            {
                for (int holeNumber = 1; holeNumber <= NumberOfHoles; holeNumber++)
                    Scores.Add(new HoleScore(player.Id, holeNumber, maxStrokes));
            }
        }

        public Guid Id { get; } = Guid.NewGuid();

        public List<Player> Players { get; init; } = new List<Player>();

        public List<HoleScore> Scores { get; init; } = new List<HoleScore>();

        public int MaxStrokes { get; init; } = 8;

        public int NumberOfHoles { get; init; } = 9;

        public int ScoreSum(Guid playerId) => Scores.Where(s => s.PlayerId == playerId).Sum(s => s.NumberOfStrokes);
    }
}