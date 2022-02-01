using GolfScoreUI.Data;
using NUnit.Framework;

namespace UnitTests;

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
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(int.MinValue)]
    public void MaxStrokesMustBePositive(int maxStrokes)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Scorecard(1, maxStrokes, new List<Player>()));
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(int.MinValue)]
    public void NumberOfHolesMustBePositive(int numberOfHoles)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Scorecard(numberOfHoles, 1, new List<Player>()));
    }

    [Test]
    public void SumsCorrectly()
    {
        var players = new List<Player> { new Player { Name = "TestPlayer" } };
        var testPlayerId = players.First().Id;
        var scorecard = new Scorecard(numberOfHoles: 9, maxStrokes: 8, players);
        var scores = new int[] { 4, 5, 3, 2, 5, 6, 8, 3, 5 };

        for (int holeNumber = 1; holeNumber <= scorecard.NumberOfHoles; holeNumber++)
            scorecard.Scores.Add(new HoleScore(testPlayerId, holeNumber, scorecard.MaxStrokes) { NumberOfStrokes = scores[holeNumber - 1] });

        Assert.AreEqual(scores.Sum(), scorecard.ScoreSum(testPlayerId));
    }
}
