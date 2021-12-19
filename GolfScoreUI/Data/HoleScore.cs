namespace GolfScoreUI.Data
{
    public class HoleScore
    {
        public Guid PlayerId { get; set; }
        public int HoleNumber { get; set; }
        public int NumberOfStrokes { get; set; }
    }
}
