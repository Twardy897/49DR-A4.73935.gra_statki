using System;
using System.Collections.Generic;
using System.Drawing;
using BattleShip.Boats;

namespace BattleShip
{
    public class Game
    {
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.White;
            
            DisplayStart();

            Player[] Players = new Player[] { new Player(), new Player() };

            SetupPlayer(Players[0], 1); //Player 1
            SetupPlayer(Players[1], 2); //Player 2

            RunGameLoop(Players);
            Console.ReadLine();
        }

        private void RunGameLoop(Player[] players)
        {
            Int32 CurrentPlayer = 0;
            Player PlayerWhoWon = null;

            while (true)
            {
                try
                {
                    Console.WriteLine("Ruch gracza {0}", players[CurrentPlayer].Name);
                    Console.WriteLine();
                    Console.WriteLine("Przeciwnik (Top)\r\n");
                    players[CurrentPlayer == 0 ? 1 : 0].DrawGrid(false);
                    Console.WriteLine();
                    Console.WriteLine("Ty (Bottom)\r\n");
                    players[CurrentPlayer].DrawGrid(true);

                    Console.WriteLine();

                    String Point = null;

                    while (String.IsNullOrEmpty(Point))
                    {
                        Console.Write("Wpisz Koordynaty (Ex : A5): ");
                        Point = Console.ReadLine();
                    }

                    Console.WriteLine();

                    Point CheckedPoint = CheckPoint(Point);
                    
                    if (players[CurrentPlayer == 0 ? 1 : 0].IsHit(CheckedPoint))
                    {
                        if (players[CurrentPlayer == 0 ? 1 : 0].AllShipsSunk)
                        {
                            PlayerWhoWon = players[CurrentPlayer];
                            break;
                        }
                    }

                    Console.WriteLine("Wcisnij Enter aby kontynuowac.");
                    Console.ReadLine();
                    CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    Console.WriteLine();
                    Console.WriteLine("Wcisnij enter aby kontynuowac.");
                    Console.ReadLine();
                }

                Console.Clear();
            }

            Console.Clear();
            Console.WriteLine("{0} wygral! Gra skonczona...", PlayerWhoWon.Name);
        }

        private void SetupPlayer(Player player, Int32 playerNumber)
        {
            Console.WriteLine("*************Ustawienia gracza {0} *************\r\n\r\n", playerNumber);

            while (String.IsNullOrEmpty(player.Name))
            {
                Console.Write("Podaj swoj NICK: ");
                player.Name = Console.ReadLine();
            }

            Console.WriteLine();

            SetupBoats(player);

            Console.WriteLine();
            Console.Clear();
        }

        private void SetupBoats(Player player)
        {
            Boat[] Boats =
            { 
                new AircraftCarrier(),
                new Battleship(),
                new Destroyer(),
                new PatrolBoat(),
                new Submarine()
            };

            Console.WriteLine("Wprowadzasz koordynaty lodzi.\r\n\r\n" +
                "Koordynaty lodzi wpisuj tak jak na przykladzie : A5;A6;A7\r\n\r\n\r\n");

            foreach (Boat Boat in Boats)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Wprowadz {0} punkty/ow dla  {1} : ", Boat.HitsAllowed, Boat.Name);
                        String PointsRead = Console.ReadLine().Trim().Replace(" ", "");

                        if (String.IsNullOrEmpty(PointsRead))
                            continue;

                        String[] Points = PointsRead.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        if (0 == Points.Length)
                            continue;

                        Boat.Points = new List<HitPoint>(Boat.HitsAllowed);

                        Int32 X = -1, Y = -1;

                        for (Int32 J = 0; J < Points.Length; J++)
                        {
                            Point CheckedPoint = CheckPoint(Points[J]);

                            if (X != -1)
                            {
                                if (Math.Abs(X - CheckedPoint.X) > 1 || Math.Abs(Y - CheckedPoint.Y) > 1)
                                    throw new Exception("Koordynaty lodzi musza byc podane w kolejnosci - BEZ PRZERWAN!.");
                            }

                            X = CheckedPoint.X;
                            Y = CheckedPoint.Y;

                            Boat.Points.Add(new HitPoint { Location = CheckedPoint });
                        }

                        player.AddBoat(Boat);

                        break;

                    }
                    catch (Exception Ex)
                    {
                        Boat.Points = null;
                        Console.WriteLine(Ex.Message);
                        continue;
                    };
                }
            }
        }

        private Point CheckPoint(String point)
        {
            if (point.Length < 2 || point.Length > 3)
                throw new Exception(String.Format("Musisz miec Literke oraz Liczbe dla kazdego Punktu {0}.", point));

            Int32 Y = Grid.GetYLetterValue(point[0].ToString());
            Int32 X = Convert.ToInt32(point.Substring(1));

            if (X < 1 || X > 10)
                throw new Exception("Wartosc moze byc od 1 do 10");

            return new Point(X - 1, Y);
        }

        private void DisplayStart()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Gra w Statki\rKacper Twardowski 73935\n\r\n\r\n");
            Console.WriteLine("Prosze o pelen ekran konsoli!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Legenda:\r\n\r\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Niezaznaczone : Puste");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Statek  : {0}", Grid.BOAT);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Chybiony  : {0}", Grid.MISS);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Uderzony   : {0}", Grid.HIT);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Wcisnij ENTER aby rozpoczac gre!");
            Console.ReadLine();
            Console.Clear();
        }

        public void Stats()
        {
            int TotalWins;
            int TotalLosses;
            double WinLossRatio;
        }
    }
}
