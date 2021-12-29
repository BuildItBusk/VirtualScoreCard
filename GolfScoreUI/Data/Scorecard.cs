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
            if (numberOfHoles <1)
                throw new ArgumentOutOfRangeException(nameof(numberOfHoles));
            if (maxStrokes < 1)
                throw new ArgumentOutOfRangeException(nameof(maxStrokes));

            Players = players ?? throw new ArgumentNullException(nameof(players));
            NumberOfHoles = numberOfHoles;
            MaxStrokes = maxStrokes;

            if (Scores.Count > 0)
                return;

            // We only need to add score, if they haven't been given in the Constructor Initialiser.
            foreach (var player in Players)
            {
                for (int i = 1; i <= NumberOfHoles; i++)
                    Scores.Add(new HoleScore { HoleNumber = i, PlayerId = player.Id });
            }
        }

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
