using System.ComponentModel.DataAnnotations;

namespace GolfScoreUI.Data
{
    public class RoundSettings
    {
        [Range(1, 10, ErrorMessage = "Must be at least 1 and at most 10.")]
        public int MaxStrokesPerHole { get; set; } = 8;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Must have a least one player to start the round.")]
        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Player3 { get; set; }

        public string Player4 {get; set; }
    }
}
