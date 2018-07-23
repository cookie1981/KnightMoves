using System.Collections.Generic;
using System.Linq;

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

        public List<Coordinates> EvaluatePossibleMoves(Coordinates currentLocation, IEnumerable<(int X, int Y)> possibleMoves)
        {
            var possibleCoordinates = possibleMoves.Select(theoreticalMove =>
                    currentLocation.RelativeCoordinates(theoreticalMove.X, theoreticalMove.Y))
                .ToList();

            var validMoves = new List<Coordinates>();

            foreach (var availableMove in possibleCoordinates)
            {
                if (LocationExists(availableMove))
                {
                    validMoves.Add(availableMove);
                }
            }

            return validMoves;
        }
    }
}