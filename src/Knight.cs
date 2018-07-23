using System;
using System.Collections.Generic;

namespace KnightMoves
{
    public class Knight : IChessPiece<Knight>
    {
        private readonly IChessBoard _chessboard;

        public Knight(Coordinates startCoordinates, IChessBoard chessboard)
        {
            _chessboard = chessboard ?? throw new ArgumentNullException(nameof(chessboard));

            if (!_chessboard.LocationExists(startCoordinates))
                throw new InvalidStartLocationException(startCoordinates);

            Location = startCoordinates;
        }

        public Knight Move(Coordinates newCoordinates)
        {
            if (AvailableMoves.Contains(newCoordinates))
            {
                return new Knight(newCoordinates, _chessboard);
            }

            throw new InvalidMoveException();
        }

        private static IEnumerable<(int X, int Y)> TheoreticalMoves
        {
            get
            {
                return new List<(int,int)>
                {
                    (1, 2),
                    (2, 1),
                    (-1, 2),
                    (2, -1),
                    (-2, 1),
                    (1, -2),
                    (-1, -2),
                    (-2, -1)
                };
            }
        }

        public Coordinates Location { get; }

        public List<Coordinates> AvailableMoves => _chessboard.EvaluatePossibleMoves(Location, TheoreticalMoves);
    }
}