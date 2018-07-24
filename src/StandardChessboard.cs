using System.Collections.Generic;
using System.Linq;

namespace KnightMoves
{
    public class StandardChessboard : IChessBoard
    {
        private int _height = 8;
        private int _width = 8;

        public bool LocationExists(Coordinates coordinates)
        {
            return coordinates.X >= 1 && coordinates.X <= _width && coordinates.Y >= 1 && coordinates.Y <= _height;
        }

        public List<Coordinates> EvaluatePossibleMoves(Coordinates currentLocation, IEnumerable<(int X, int Y)> possibleMoves)
        {
            return possibleMoves.Select(move => currentLocation.RelativeCoordinates(move.X, move.Y)).Where(LocationExists).ToList();
        }
    }

    public class CheesBoardWithTracking : IChessBoard, IMovementTracker
    {
        private int _height = 8;
        private int _width = 8;
        private List<Coordinates> _usedLocations = new List<Coordinates>();

        public bool LocationExists(Coordinates coordinates)
        {
            return coordinates.X >= 1 && coordinates.X <= _width && coordinates.Y >= 1 && coordinates.Y <= _height;
        }

        public List<Coordinates> EvaluatePossibleMoves(Coordinates currentLocation, IEnumerable<(int X, int Y)> possibleMoves)
        {
            var validMovesInUnusedLocations = new List<Coordinates>();
            var validMoves = possibleMoves.Select(move => currentLocation.RelativeCoordinates(move.X, move.Y)).Where(LocationExists).ToList();

            foreach (var coordinate in validMoves)
            {
                if (!_usedLocations.Contains(coordinate))
                {
                    validMovesInUnusedLocations.Add(coordinate);
                }
            }

            return validMovesInUnusedLocations;
        }

        public void LogLocation(Coordinates newLocation)
        {
            _usedLocations.Add(newLocation);
        }
    }

    public interface IMovementTracker
    {
        void LogLocation(Coordinates newLocation);
    }
}