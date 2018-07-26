using System;
using System.Collections.Generic;

namespace KnightMoves
{
    public class KnightV2 : IChessPiece<KnightV2>, IObservable<Coordinates>
    {
        private readonly IChessBoard _chessboard;
        private readonly List<IObserver<Coordinates>> _observers;
        private readonly bool _isStartPosition;

        public KnightV2(Coordinates startCoordinates, IChessBoard chessboard)
        {
            _chessboard = chessboard ?? throw new ArgumentNullException(nameof(chessboard));

            if (!_chessboard.LocationExists(startCoordinates))
                throw new InvalidStartLocationException(startCoordinates);

            _isStartPosition = true;
            Location = startCoordinates;

            _observers = new List<IObserver<Coordinates>>();
        }

        private KnightV2(Coordinates newCoordinates, IChessBoard chessboard, List<IObserver<Coordinates>> observers = null)
        {
            _chessboard = chessboard ?? throw new ArgumentNullException(nameof(chessboard));

            if (!_chessboard.LocationExists(newCoordinates))
                throw new InvalidStartLocationException(newCoordinates);

            _isStartPosition = false;
            Location = newCoordinates;

            _observers = observers ?? new List<IObserver<Coordinates>>();
            PublishMovement(newCoordinates);
        }

        public KnightV2 Move(Coordinates newCoordinates)
        {
            if (!AvailableMoves.Contains(newCoordinates)) throw new InvalidMoveException();

            if (_isStartPosition)
            {
                PublishMovement(Location);
            }

            return new KnightV2(newCoordinates, _chessboard, _observers);
        }

        private void PublishMovement(Coordinates newCoordinates)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(newCoordinates);
            }
        }

        private static IEnumerable<(int X, int Y)> TheoreticalMoves
        {
            get
            {
                return new List<(int, int)>
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

        public IDisposable Subscribe(IObserver<Coordinates> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Coordinates>> _observers;
            private IObserver<Coordinates> _observer;

            public Unsubscriber(List<IObserver<Coordinates>> observers, IObserver<Coordinates> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

    }
}