namespace GolfScoreUI.Data
{
    public record HoleScore
    {
        public Guid PlayerId { get; set; }
        public int HoleNumber { get; set; }
        public int NumberOfStrokes { get; set; } = 0;
    }
}
