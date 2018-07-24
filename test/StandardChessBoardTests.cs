using KnightMoves;
using NUnit.Framework;

namespace KnightMovesTests
{
    public class StandardChessBoardTests
    {
        [Test]
        public void ShouldDefaultHeightAndWidthTo8WhenNoSupplidOnConstructor()
        {
            var chessboard = new StandardChessboard();

            Assert.That(chessboard.Height, Is.EqualTo(8));
            Assert.That(chessboard.Width, Is.EqualTo(8));
        }

        [Test]
        public void ShouldSetCustomerHeightAndWidth()
        {
            const int height = 4;
            const int width = 5;
            var chessboard = new StandardChessboard(height, width);

            Assert.That(chessboard.Height, Is.EqualTo(height));
            Assert.That(chessboard.Width, Is.EqualTo(width));
        }

        //TODO: Write tests for chessboard methods
    }
}