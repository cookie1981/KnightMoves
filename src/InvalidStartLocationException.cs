using System;

namespace KnightMoves
{
    public class InvalidStartLocationException : Exception
    {
        public InvalidStartLocationException(Coordinates coordinates)
        {
        }
    }
}