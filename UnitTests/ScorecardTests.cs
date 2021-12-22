using GolfScoreUI.Data;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    internal class ScorecardTests
    {
        [Test]
        public void CanSumCorrectly()
        {
            var player = new Player();
            var players = new List<Player>();
            players.Add(player);
            var scorecard = new Scorecard(9, 8, players);

            for (int i = 1; i <= scorecard.NumberOfHoles; i++)
                scorecard.SetScore(player.Id, i, i);

            var expected = 45;
            var actual =  scorecard.ScoreSum(player.Id);

            Assert.AreEqual(expected, actual);
        }
    }
}
