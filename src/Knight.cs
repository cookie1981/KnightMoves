using System;
using System.Collections.Generic;

namespace KnightMoves
{
    public class Knight : IChessPiece<Knight>
    {
        private readonly IChessBoard _chessboard;
        private readonly IMovementTracker _movementTracker;

        public Knight(Coordinates startCoordinates, IChessBoard chessboard, IMovementTracker movementTracker)
        {
            _chessboard = chessboard ?? throw new ArgumentNullException(nameof(chessboard));
            _movementTracker = movementTracker ?? throw new ArgumentNullException(nameof(movementTracker));

            if (!_chessboard.LocationExists(startCoordinates))
                throw new InvalidStartLocationException(startCoordinates);

            Location = startCoordinates;
            _movementTracker.LogLocation(startCoordinates);
        }

        public Knight Move(Coordinates newCoordinates)
        {
            if (!AvailableMoves.Contains(newCoordinates)) throw new InvalidMoveException();

            _movementTracker.LogLocation(newCoordinates);

            return new Knight(newCoordinates, _chessboard, _movementTracker);
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