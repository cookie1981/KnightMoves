using KnightMoves;
using NUnit.Framework;

namespace KnightMovesTests
{
    public class CoordinatesTests
    {
        [TestCase(0, 0, 1, 1, 1, 1)]
        [TestCase(0, 0, -1, -1, -1, -1)]
        [TestCase(4, 4, 1, 1, 5, 5)]
        [TestCase(4, 4, -1, -1, 3, 3)]
        [TestCase(4, 4, -1, 1, 3, 5)]
        [TestCase(4, 4, 1, -1, 5, 3)]
        [TestCase(4, 4, -1, 1, 3, 5)] //needs a better name!
        public void ShouldCreateCoordinatesFromTupleOfIntInt(int startX, int startY, int moveFromStartCoordinatesByX, int moveFromStartCoordinatesByY, int expectedNewX, int expectedNewY)
        {
            var currentLocation = new Coordinates(startX, startY);

            var relativeCoordinates = currentLocation.RelativeCoordinates(moveFromStartCoordinatesByX, moveFromStartCoordinatesByY);

            Assert.That(relativeCoordinates.X, Is.EqualTo(expectedNewX));
            Assert.That(relativeCoordinates.Y, Is.EqualTo(expectedNewY));
        }
    }
}