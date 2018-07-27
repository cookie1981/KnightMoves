using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightMoves
{
    public class TrackingChessBoardV2 : IChessBoard, IObserver<Coordinates>
    {
        private readonly IChessBoard _chessBoard;
        private IDisposable _unsubscriber;

        public TrackingChessBoardV2(IChessBoard chessBoard)
        {
            _chessBoard = chessBoard;
            UsedLocations = new List<Coordinates>();
        }

        public List<Coordinates> UsedLocations { get; }

        public bool LocationExists(Coordinates coordinates)
        {
            return _chessBoard.LocationExists(coordinates);
        }

        public List<Coordinates> EvaluatePossibleMoves(Coordinates currentLocation, IEnumerable<(int X, int Y)> possibleMoves)
        {
            var validMovesInUnusedLocations = new List<Coordinates>();
            var validMoves = _chessBoard.EvaluatePossibleMoves(currentLocation, possibleMoves);

            foreach (var coordinate in validMoves)
            {

                if (!UsedLocations.Any(x => x.Equals(coordinate)))
                {
                    validMovesInUnusedLocations.Add(coordinate);
                }
            }

            return validMovesInUnusedLocations;
        }

        public virtual void Subscribe(IObservable<Coordinates> provider)
        {
            if (provider != null)
                _unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Coordinates value)
        {
            if (!UsedLocations.Contains(value))
                UsedLocations.Add(value);
        }
    }
}