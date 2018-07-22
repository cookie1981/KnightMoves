using KnightMoves;
using Moq;
using NUnit.Framework;

namespace KnightMovesTests
{
    public class KnightTests
    {
        private Chessboard _emptyChessboard;
        private readonly Coordinates _topLeftCorner = new Coordinates(1, 1);

        [SetUp]
        public void Setup()
        {
            _emptyChessboard = new Chessboard();
        }

        [Test]
        public void ShouldThrowWhenStartCoordinatesAreOutOfBoundsForTheChessboard()
        {
            Assert.Throws<InvalidStartLocationException>(() => new Knight(new Coordinates(0, 0), _emptyChessboard));
        }

        [Test]
        public void ShouldThrowWhenBoardIsNull()
        {
            Assert.That(() => new Knight(_topLeftCorner, null), Throws.ArgumentNullException);
        }

        [Test]
        public void ShouldInitiailiseCoordinates()
        {
            var knight = new Knight(_topLeftCorner, _emptyChessboard);

            Assert.That(knight.Location, Is.EqualTo(_topLeftCorner));
        }

        [Test]
        public void ShouldOnlyEvaluateAvailableMovesOnFirstRequest()
        {
            const int numberOfPossibleMoves = 8;
            const int callsFromTheConstructor = 1;
            const int callsFromTheMoveMethod = 1;
            const int expectedCallCount = numberOfPossibleMoves + callsFromTheConstructor + callsFromTheMoveMethod;
            var chessboard = new Mock<IChessBoard>();

            chessboard.Setup(x => x.LocationExists(It.IsAny<Coordinates>())).Returns(true);

            var knight = new Knight(_topLeftCorner, chessboard.Object);

            var availableMoves = knight.AvailableMoves;
            availableMoves = knight.AvailableMoves;
            knight.Move(new Coordinates(2, 3));
           
            chessboard.Verify(x => x.LocationExists(It.IsAny<Coordinates>()), Times.Exactly(expectedCallCount));
        }

        [Test]
        public void ShouldListAllAvailableMovesOnAnEmptyBoard()
        {
            var knight = new Knight(new Coordinates(4, 4), _emptyChessboard);

            Assert.That(knight.AvailableMoves.Count, Is.EqualTo(8));
            //should i test that the coords are as expected? is this duplication?
        }

        [Test]
        public void ShouldNotListPossibleMoveWhenNewLocationIsOffTheBoard()
        {
            var knight = new Knight(_topLeftCorner, _emptyChessboard);

            Assert.That(knight.AvailableMoves.Count, Is.EqualTo(2));
            Assert.That(knight.AvailableMoves[0].X, Is.EqualTo(2));
            Assert.That(knight.AvailableMoves[0].Y, Is.EqualTo(3));
            Assert.That(knight.AvailableMoves[1].X, Is.EqualTo(3));
            Assert.That(knight.AvailableMoves[1].Y, Is.EqualTo(2));
        }

        [Test]
        public void ShouldMoveKnight()
        {
            var knight = new Knight(_topLeftCorner, _emptyChessboard);

            var newCoordinates = new Coordinates(2,3);

            var movedKnight = knight.Move(newCoordinates);

            Assert.That(movedKnight.Location, Is.EqualTo(newCoordinates));
        }

        [Test]
        public void ShouldThrowIfAttemptedMoveIsInvalid()
        {
            var knight = new Knight(_topLeftCorner, _emptyChessboard);

            Assert.Throws<InvalidMoveException>(() => knight.Move(new Coordinates(9,9)));
        }
    }
}
