using System.ComponentModel.DataAnnotations;

namespace GolfScoreUI.Data
{
    public class RoundSettings
    {
        [Range(1, 10, ErrorMessage = "Must be at least 1 and at most 10.")]
        public int MaxStrokesPerHole { get; set; } = 8;

        [Range(1, 18, ErrorMessage = "Must be between 1 and 18.")]
        public int NumberOfHoles { get; set; } = 9;

        public List<Player> Players { get; } = new List<Player> { new Player { Name = "" }, new Player { Name = "" }, new Player { Name = "" }, new Player { Name = "" } };
    }
}
