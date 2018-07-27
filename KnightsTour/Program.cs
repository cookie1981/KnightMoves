using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using KnightMoves;

namespace KnightsTour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var trackerBoard = new TrackingChessBoardV2(new StandardChessboard());
            var knight = new KnightV2(new Coordinates(1,1), trackerBoard);

            var moveEvents = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(_ => knight.Location)
                .Publish();
            knight.Subscribe(trackerBoard);

            using (moveEvents.Subscribe(trackerBoard))
            {
                moveEvents.Connect();

                var exit = false;

                while (!exit)
                {
                    knight = Play(knight, trackerBoard, out exit);
                    Console.WriteLine(LocationDescription(knight.Location));
                }
            }
        }

        private static string LocationDescription(Coordinates location)
        {
            return $"(X: {location.X}, Y:{location.Y}), ";
        }

        private static KnightV2 Play(KnightV2 knight, TrackingChessBoardV2 trackerBoard, out bool exit)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Path so far:");
                ListCoordinates(trackerBoard.UsedLocations);

                Console.WriteLine();
                Console.WriteLine("Current Location:");
                Console.WriteLine(LocationDescription(knight.Location));

                Console.WriteLine();
                Console.WriteLine("AvailableMoves: ");
                ListCoordinates(knight.AvailableMoves);

                Console.WriteLine();
                Console.Write("> ");
                var command = Console.ReadLine();

                if (command == "exit")
                {
                    exit = true;
                    break;
                }

                var strings = command.Split(',');
                var x = Convert.ToInt32(strings[0]);
                var y = Convert.ToInt32(strings[1]);
                var move = new Coordinates(x, y);

                if (!knight.AvailableMoves.Contains(move))
                {
                    Console.WriteLine("Invalid move!");
                    continue;
                }

                knight = knight.Move(move);

                exit = false;
            }

            return knight;
        }

        private static void ListCoordinates(IEnumerable<Coordinates> listOfCoordinates)
        {
            foreach (var coordinate in listOfCoordinates)
            {
                Console.WriteLine(LocationDescription(coordinate));
            }
        }
    }
}
