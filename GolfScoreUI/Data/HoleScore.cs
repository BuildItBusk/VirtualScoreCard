namespace GolfScoreUI.Data
{
    public record HoleScore
    {
        private int _numberOfStrokes;

        public HoleScore(Guid playerId, int holeNumber, int maxStrokes)
        {
            PlayerId = playerId;
            HoleNumber = holeNumber > 0 ? holeNumber : throw new ArgumentOutOfRangeException(nameof(holeNumber));
            MaxStrokes = maxStrokes > 0 ? maxStrokes : throw new ArgumentOutOfRangeException(nameof(maxStrokes));
        }

        public Guid PlayerId { get; }
        public int HoleNumber { get; }
        public bool IsMax => NumberOfStrokes >= MaxStrokes;
        public int MaxStrokes { get; }
        public int NumberOfStrokes
        {
            get => _numberOfStrokes;
            set => _numberOfStrokes = Math.Min(value, MaxStrokes);
        }
    }
}
