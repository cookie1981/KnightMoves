using System.Collections.Generic;

namespace KnightMoves
{
    public interface IChessBoard
    {
        bool LocationExists(Coordinates coordinates);

        List<Coordinates> EvaluatePossibleMoves(Coordinates currentLocation, IEnumerable<(int X, int Y)> possibleMoves);
    }
}