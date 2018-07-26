using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightMoves
{
    public class TrackingChessBoardV1 : IChessBoard, IMovementTracker
    {
        private readonly IChessBoard _decoratedChessBoard;

        public TrackingChessBoardV1(IChessBoard chessBoard)
        {
            _decoratedChessBoard = chessBoard ?? throw new ArgumentNullException(nameof(chessBoard));
            UsedLocations = new List<Coordinates>();
        }

        public bool LocationExists(Coordinates coordinates)
        {
            return _decoratedChessBoard.LocationExists(coordinates);
        }

        public List<Coordinates> EvaluatePossibleMoves(Coordinates currentLocation, IEnumerable<(int X, int Y)> possibleMoves)
        {
            var validMovesInUnusedLocations = new List<Coordinates>();
            var validMoves = _decoratedChessBoard.EvaluatePossibleMoves(currentLocation, possibleMoves);

            foreach (var coordinate in validMoves)
            {
                if (!UsedLocations.Any(x => x.Equals(coordinate)))
                {
                    validMovesInUnusedLocations.Add(coordinate);
                }
            }

            return validMovesInUnusedLocations;
        }

        public void LogLocation(Coordinates newLocation)
        {
            UsedLocations.Add(newLocation);
        }

        public List<Coordinates> UsedLocations { get; }
    }
}