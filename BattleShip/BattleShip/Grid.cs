using System;
using System.Drawing;

namespace BattleShip
{
    public class Grid
    {
        public const String UNPINNED = " ";
        public const String MISS = "O";
        public const String BOAT = "$";
        public const String HIT = "*";

        private GridPoint[,] _Points = new GridPoint[10, 10];

        private static readonly String[] _Letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


        public void SetPin(Point point, GridPoint pointValue)
        {
            if (_Points[point.X, point.Y] != GridPoint.UnPinned && _Points[point.X, point.Y] != GridPoint.Boat)
                throw new Exception("Ten punkt jest juz uzywany!.");

            _Points[point.X, point.Y] = pointValue;
        }


        public void DrawGrid(Boolean playerView)
        {
            Console.Write(" |");

            for (Int32 X = 1; X < 11; X++)
                Console.Write("{0}|", X);

            Console.WriteLine();
            Console.WriteLine("----------------------");

            for (Int32 Y = 0; Y < 10; Y++)
            {
                Console.Write("{0}|", _Letters[Y]);

                for (Int32 X = 1; X < 11; X++)
                {
                    GridPoint Point = _Points[X - 1, Y];
                    Console.ForegroundColor = ConsoleColor.Green;

                    switch (Point)
                    {
                        case GridPoint.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(HIT);
                            break;
                        case GridPoint.Miss:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(MISS);
                            break;
                        case GridPoint.Boat:
                            Console.ForegroundColor = playerView ? ConsoleColor.Yellow : ConsoleColor.Green;
                            Console.Write(playerView ? BOAT : UNPINNED);
                            break;
                        default: //unpinned
                            Console.Write(UNPINNED);
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------");
        }


        public static Int32 GetYLetterValue(String letter)
        {
            if (String.IsNullOrEmpty(letter))
                throw new ArgumentException("letter");

            Int32 Index = Array.IndexOf(_Letters, letter.ToUpper());

            if (-1 != Index)
                return Index;

            throw new Exception("Mozesz wprowadzic koordynaty od A do J.");
        }
    }
}
