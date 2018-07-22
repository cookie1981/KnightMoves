using System;
using System.Collections.Generic;
using System.Linq;

namespace KnightMoves
{
    public class Rook : IChessPiece<Rook>
    {
        private readonly IChessBoard _chessboard;
        private List<Coordinates> _availableMoves = new List<Coordinates>();
        private bool _alreadyEvaluated;

        public Rook(Coordinates startCoordinates, IChessBoard chessboard)
        {
            _chessboard = chessboard ?? throw new ArgumentNullException(nameof(chessboard));

            if (!_chessboard.LocationExists(startCoordinates))
                throw new InvalidStartLocationException(startCoordinates);

            Location = startCoordinates;
        }

        public List<Coordinates> AvailableMoves => EvaluatePossibleMoves();

        public Coordinates Location { get; }

        public Rook Move(Coordinates newCoordinates)
        {
            if (AvailableMoves.Contains(newCoordinates))
            {
                return new Rook(newCoordinates, _chessboard);
            }

            throw new InvalidMoveException();
        }

        private List<Coordinates> EvaluatePossibleMoves()
        {
            var validMoves = new List<Coordinates>();

            if (_alreadyEvaluated)
                return _availableMoves;

            var possibleMoves = TheoreticalMoves.Select(theoreticalMove =>
                                                    Location.RelativeCoordinates(theoreticalMove.X, theoreticalMove.Y))
                                                    .ToList();

          //  _availableMoves = _chessboard.EvaluatePossibleMoves(possibleMoves);

            foreach (var availableMove in possibleMoves)
            {
                if (_chessboard.LocationExists(availableMove))
                {
                    validMoves.Add(availableMove);
                }
            }

            _availableMoves = validMoves;
            _alreadyEvaluated = true;

            return validMoves;
        }

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

    //can I make knight immutable again? - AlreadyEvaluated
    public class Knight : IChessPiece<Knight>
    {
        private readonly IChessBoard _chessboard;
        private List<Coordinates> _availableMoves = new List<Coordinates>();
        private bool _alreadyEvaluated;

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

        private List<Coordinates> EvaluatePossibleMoves()
        {
            var validMoves = new List<Coordinates>();

            if (_alreadyEvaluated)
                return _availableMoves;

            var possibleMoves = TheoreticalMoves.Select(theoreticalMove =>
                                                    Location.RelativeCoordinates(theoreticalMove.X, theoreticalMove.Y))
                                                    .ToList();

            foreach (var availableMove in possibleMoves)
            {
                if (_chessboard.LocationExists(availableMove))
                {
                    validMoves.Add(availableMove);
                }
            }

            _availableMoves = validMoves;
            _alreadyEvaluated = true;

            return validMoves;
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

        public List<Coordinates> AvailableMoves => EvaluatePossibleMoves();
    }
}