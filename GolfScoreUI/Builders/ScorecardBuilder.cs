using GolfScoreUI.Data;

namespace GolfScoreUI.Builders
{
    public static class ScorecardBuilder
    {
        public static Scorecard NewScorecard(int numberOfHoles, int maxStrokes, List<Player> players)
        {
            Scorecard scorecard = new()
            {
                NumberOfHoles = numberOfHoles,
                MaxStrokes = maxStrokes,
                Players = players
            };

            foreach (var player in scorecard.Players)
            {
                for (int i = 1; i <= scorecard.NumberOfHoles; i++)
                    scorecard.Scores.Add(new HoleScore { HoleNumber = i, PlayerId = player.Id });
            }

            return scorecard;
        }
    }
}
