using GolfScoreUI.Data;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    internal class ScorecardTests
    {
        [Test]
        [TestCase(8, 18)]
        [TestCase(15, 9)]
        [TestCase(5, 1)]
        public void AcceptsValidInput(int maxStrokes, int numberOfHoles)
        {
            var scorecard = new Scorecard(numberOfHoles, maxStrokes, new List<Player>());
            
            Assert.AreEqual(maxStrokes, scorecard.MaxStrokes);
            Assert.AreEqual(numberOfHoles, scorecard.NumberOfHoles);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(-5, 1)]
        [TestCase(1, -5)]
        public void PreventsInvalidValues(int maxStrokes, int numberOfHoles)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Scorecard(numberOfHoles, maxStrokes, new List<Player>()));
        }
    }
}
