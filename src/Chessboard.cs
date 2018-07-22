namespace KnightMoves
{
    public class Chessboard : IChessBoard
    {
        private int _height = 8;
        private int _width = 8;

        public Chessboard()
        {
        }

        public bool LocationExists(Coordinates coordinates)
        {
            return coordinates.X >= 1 && coordinates.X <= _width && coordinates.Y >= 1 && coordinates.Y <= _height;
        }
    }
}