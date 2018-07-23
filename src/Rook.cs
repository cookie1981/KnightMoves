using System;
using System.Collections.Generic;

namespace KnightMoves
{
    public class Rook : IChessPiece<Rook>
    {
        private readonly IChessBoard _chessboard;

        public Rook(Coordinates startCoordinates, IChessBoard chessboard)
        {
            _chessboard = chessboard ?? throw new ArgumentNullException(nameof(chessboard));

            if (!_chessboard.LocationExists(startCoordinates))
                throw new InvalidStartLocationException(startCoordinates);

            Location = startCoordinates;
        }

        public Rook Move(Coordinates newCoordinates)
        {
            if (AvailableMoves.Contains(newCoordinates))
            {
                return new Rook(newCoordinates, _chessboard);
            }

            throw new InvalidMoveException();
        }

        public List<Coordinates> AvailableMoves => _chessboard.EvaluatePossibleMoves(Location, TheoreticalMoves);

        public Coordinates Location { get; }

        private static IEnumerable<(int X, int Y)> TheoreticalMoves
        {
            get
            {
                return new List<(int, int)>
                {
                    //Refacotr this
                    (1, 0),
                    (2, 0),
                    (3, 0),
                    (4, 0),
                    (5, 0),
                    (6, 0),
                    (7, 0),
                    (-1, 0),
                    (-2, 0),
                    (-3, 0),
                    (-4, 0),
                    (-5, 0),
                    (-6, 0),
                    (-7, 0),
                    (0, 1),
                    (0, 2),
                    (0, 3),
                    (0, 4),
                    (0, 5),
                    (0, 6),
                    (0, 7),
                    (0, -1),
                    (0, -2),
                    (0, -3),
                    (0, -4),
                    (0, -5),
                    (0, -6),
                    (0, -7),
                };
            }
        }
    }
}