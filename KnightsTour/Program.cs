using System;
using System.Reactive.Linq;
using KnightMoves;

namespace KnightsTour
{
    class Program
    {
        static void Main(string[] args)
        {
            var chessBoard = new StandardChessboard();
            var tracker = new TrackingChessBoardV2(chessBoard);

            var knight = new KnightV2(new Coordinates(1, 1), chessBoard); ;
            tracker.Subscribe(knight);

            var movementEvents = Observable
                .Interval(TimeSpan.FromMilliseconds(1))
                .Select(_ => knight.Location)
                .Publish();

            using (movementEvents.Subscribe(tracker))
            {
                movementEvents.Connect();
            }

            knight = knight.Move(new Coordinates(2, 3));
            knight = knight.Move(new Coordinates(1, 5));
            knight = knight.Move(new Coordinates(2, 7));
            knight = knight.Move(new Coordinates(4, 8));

            Console.ReadLine();
        }
    }
}
