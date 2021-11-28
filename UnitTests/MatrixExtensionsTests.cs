using GolfScoreUI.Extensions;
using NUnit.Framework;

namespace UnitTests
{
    public class MatrixExtensionsTests
    {
        // 1 2 3 4
        // 5 6 7 8
        private readonly int[,] two_by_four = new int[2, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };

        // 0 1 2
        // 3 4 5
        // 6 7 8
        private readonly int[,] three_by_three = new int[3,3]{ { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };

        [Test]
        public void CanSumRows()
        {
            int[] expected = new int[] { 6, 8, 10, 12 };
            int[] actual = two_by_four.Sum(0);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanSumCols()
        {
            int[] expected = new int[] { 10, 26 };
            int[] actual = two_by_four.Sum(1);
        }
    }
}