using System.Collections.Generic;
using System.Linq;

namespace KnightMoves
{
    public class StandardChessboard : IChessBoard
    {
        public StandardChessboard()
        {
            Height = Width = 8;
        }

        public StandardChessboard(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public bool LocationExists(Coordinates coordinates)
        {
            return coordinates.X >= 1 && coordinates.X <= Width && coordinates.Y >= 1 && coordinates.Y <= Height;
        }

        public List<Coordinates> EvaluatePossibleMoves(Coordinates currentLocation, IEnumerable<(int X, int Y)> possibleMoves)
        {
            return possibleMoves.Select(move => currentLocation.RelativeCoordinates(move.X, move.Y)).Where(LocationExists).ToList();
        }

        public int Height { get; }

        public int Width { get; }
    }
}