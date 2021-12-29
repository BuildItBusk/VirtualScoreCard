using GolfScoreUI.Data;

namespace GolfScoreUI.Builders
{
    public static class ScorecardBuilder
    {
        public static Scorecard NewScorecard(int numberOfHoles, int maxStrokes, List<Player> players)
        {
            return new Scorecard(numberOfHoles, maxStrokes, players);
        }
    }
}
