using System.Collections.Generic;

namespace KnightMoves
{
    public interface IChessPiece<T>
    {
        T Move(Coordinates newCoordinates);

        List<Coordinates> AvailableMoves { get; }

        Coordinates Location { get; }
    }
}