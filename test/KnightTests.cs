using KnightMoves;
using NUnit.Framework;

namespace KnightMovesTests
{
    public class KnightTests
    {
        private StandardChessboard  _emptyStandardChessboard;
        private TrackingCheesBoard _trackingChessboard;
        private readonly Coordinates _topLeftCorner = new Coordinates(1, 1);

        [SetUp]
        public void Setup()
        {
            _emptyStandardChessboard = new StandardChessboard();
            _trackingChessboard = new TrackingCheesBoard(_emptyStandardChessboard);
        }

        [Test]
        public void ShouldThrowWhenStartCoordinatesAreOutOfBoundsForTheChessboard()
        {
            Assert.Throws<InvalidStartLocationException>(() => new Knight(new Coordinates(0, 0), _trackingChessboard, _trackingChessboard));
        }

        [Test]
        public void ShouldThrowWhenBoardIsNull()
        {
            Assert.That(() => new Knight(_topLeftCorner, null, _trackingChessboard), Throws.ArgumentNullException);
        }

        [Test]
        public void ShouldThrowWhenMovementTrackerIsNull()
        {
            Assert.That(() => new Knight(_topLeftCorner, _trackingChessboard, null), Throws.ArgumentNullException);
        }

        [Test]
        public void ShouldInitiailiseCoordinates()
        {
            var knight = new Knight(_topLeftCorner, _trackingChessboard, _trackingChessboard);

            Assert.That(knight.Location, Is.EqualTo(_topLeftCorner));
        }

//        [Test]
//        public void ShouldOnlyEvaluateAvailableMovesOnFirstRequest()
//        {
//            const int numberOfPossibleMoves = 8;
//            const int callsFromTheConstructor = 1;
//            const int callsFromTheMoveMethod = 1;
//            const int expectedCallCount = numberOfPossibleMoves + callsFromTheConstructor + callsFromTheMoveMethod;
//            var chessboard = new Mock<IChessBoard>();
//
//            chessboard.Setup(x => x.LocationExists(It.IsAny<Coordinates>())).Returns(true);
//
//            var knight = new Knight(_topLeftCorner, chessboard.Object);
//
//            var availableMoves = knight.AvailableMoves;
//            availableMoves = knight.AvailableMoves;
//            knight.Move(new Coordinates(2, 3));
//           
//            chessboard.Verify(x => x.LocationExists(It.IsAny<Coordinates>()), Times.Exactly(expectedCallCount));
//        }

        [Test]
        public void ShouldListAllAvailableMovesOnAnEmptyBoard()
        {
            var knight = new Knight(new Coordinates(4, 4), _trackingChessboard, _trackingChessboard);

            Assert.That(knight.AvailableMoves.Count, Is.EqualTo(8));
            //should i test that the coords are as expected? is this duplication?
        }

        [Test]
        public void ShouldNotListPossibleMoveWhenNewLocationIsOffTheBoard()
        {
            var knight = new Knight(_topLeftCorner, _trackingChessboard, _trackingChessboard);

            Assert.That(knight.AvailableMoves.Count, Is.EqualTo(2));
            Assert.That(knight.AvailableMoves[0].X, Is.EqualTo(2));
            Assert.That(knight.AvailableMoves[0].Y, Is.EqualTo(3));
            Assert.That(knight.AvailableMoves[1].X, Is.EqualTo(3));
            Assert.That(knight.AvailableMoves[1].Y, Is.EqualTo(2));
        }

        [Test]
        public void ShouldMoveKnight()
        {
            var knight = new Knight(_topLeftCorner, _trackingChessboard, _trackingChessboard);

            var newCoordinates = new Coordinates(2,3);

            var movedKnight = knight.Move(newCoordinates);

            Assert.That(movedKnight.Location, Is.EqualTo(newCoordinates));
        }

        [Test]
        public void ShouldThrowIfAttemptedMoveIsInvalid()
        {
            var knight = new Knight(_topLeftCorner, _trackingChessboard, _trackingChessboard);

            Assert.Throws<InvalidMoveException>(() => knight.Move(new Coordinates(9,9)));
        }

        [Test]
        public void ShouldNotListPreviouslyVisitedLocationsInAvailableMoves()
        {
            var knight = new Knight(_topLeftCorner, _trackingChessboard, _trackingChessboard);

            knight = knight.Move(new Coordinates(3, 2));

            Assert.False(knight.AvailableMoves.Contains(_topLeftCorner));
            Assert.That(knight.AvailableMoves.Count, Is.EqualTo(5));
        }
    }
}
