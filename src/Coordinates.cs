namespace KnightMoves
{
    public struct Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public Coordinates RelativeCoordinates(int distanceFromCurrentCoordinatesOnXAxis, int distanceFromCurrentCoordinatesOnYAxis)
        {
            var newX = X + distanceFromCurrentCoordinatesOnXAxis;
            var newY = Y + distanceFromCurrentCoordinatesOnYAxis;

            return new Coordinates(newX, newY);
        }
    }
}