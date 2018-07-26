using System;
using KnightMoves;
using NUnit.Framework;

namespace KnightMovesTests
{
    public class ChessBoardWithTrackingTests
    {
        private readonly IChessBoard _chessBoard = new StandardChessboard();

        [Test]
        public void ShouldThrowWhenChessBoardIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TrackingChessBoardV1(null));
        }

        [Test]
        public void ShouldRecordWhichGridPositionsHaveBeenUsed()
        {
            var chessboard = new TrackingChessBoardV1(_chessBoard);
            var location = new Coordinates(1, 2);

            chessboard.LogLocation(location);

            Assert.That(chessboard.UsedLocations.Contains(location));
        }

        //TODO: Possibly write tests for the methods on the tracker that simply delegate to the decorated chessboard?
    }
}