using GolfScoreAPI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

[TestFixture]
internal class ApiTests
{
    [Test]
    public void CanCreateNewScorecard()
    {
        var player = new UserProfile("John Doe", "john.doe@gmail.com");

        var scorecard = new Scorecard(player, 18);

        Assert.AreEqual("John Doe", scorecard.PlayerNames[0]);
        Assert.AreEqual(18, scorecard.NumberOfHoles);
    }

    [Test]
    public void CreateUserFailsOnEmptyName()
    {
        Assert.Throws<ArgumentNullException>(() => new UserProfile("", "john.doe@gmail.com"));        
    }

    [Test]
    public void CreateUserFailsOnEmptyEmail()
    {
        Assert.Throws<ArgumentNullException>(() => new UserProfile("John Doe", ""));
    }

    [Test]
    public void CanAssignValidScore()
    {
        var user = new UserProfile("John Doe", "john.doe@gmail.com");
        var scorecard = new Scorecard(user, numberOfHoles: 18);

        scorecard.AssignScore(user.Username, hole: 1, strokes: 3);

        Assert.AreEqual(3, scorecard.Scores.First(s => s.PlayerName == user.Username && s.HoleNumber == 1));
    }

    [Test]
    public void CannotAssignNegativeScore()
    {
        var user = new UserProfile("John Doe", "john.doe@gmail.com");
        var scorecard = new Scorecard(user, numberOfHoles: 18);
        
        Assert.Throws<ArgumentOutOfRangeException>(
            () => scorecard.AssignScore(user.Username, hole: 1, strokes: -1));
    }

    [Test]
    public void CannotAssignScoreToInvalidHole()
    {
        var user = new UserProfile("John Doe", "john.doe@gmail.com");
        var scorecard = new Scorecard(user, numberOfHoles: 18);

        // There are only 18 holes on the scorecard, so assigning to hole 19 shoudn't be possible
        Assert.Throws<ArgumentOutOfRangeException>(
            () => scorecard.AssignScore(user.Username, hole: 19, strokes: 3));
    }

    [Test]
    public void CannotAssignScoreToNegativeHoleNumber()
    {
        var user = new UserProfile("John Doe", "john.doe@gmail.com");
        var scorecard = new Scorecard(user, numberOfHoles: 18);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => scorecard.AssignScore(user.Username, hole: -1, strokes: 3));
    }

    [Test]
    public void CanSumCorretcly()
    {
        int[] holes = Enumerable.Range(1, 9).ToArray();
        int[] scores = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int expectedSum = 45;

        var user = new UserProfile("John Doe", "john.doe@gmail.com");
        var scorecard = new Scorecard(user, 9);
        for (int i = 0; i < holes.Length; i++)
            scorecard.AssignScore(user.Username, holes[i], scores[i]);

        Assert.AreEqual(expectedSum, scorecard.ScoreSum(user.Username));
    }
}
