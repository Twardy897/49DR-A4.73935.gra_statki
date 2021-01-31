using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BattleShip
{
    public class Player
    {
        private List<Boat> _Boats = new List<Boat>(5);

        private Grid _Grid = new Grid(); 

        public String Name { get; set; }


        public Boolean AllShipsSunk { get { return _Boats.All((boat) => boat.IsSunk); } }

        public void AddBoat(Boat boat)
        {
            CheckBoat(boat);
            _Boats.Add(boat);

            foreach (HitPoint Point in boat.Points)
                _Grid.SetPin(Point.Location, GridPoint.Boat);
        }

        private void CheckBoat(Boat boat)
        {
            if (null == boat)
                throw new ArgumentNullException("statek");

            if (_Boats.Count == 5)
                throw new Exception("Siatka moze zawierac tylko 5 statkow");

            if (boat.HitsAllowed != boat.Points.Count)
                throw new Exception(String.Format("Nie ustawiles punktow, ta lodz potrzebuje{0} punktow.", boat.HitsAllowed));

            if (boat.Points.Count != boat.Points.Distinct().Count())
                throw new Exception(String.Format("Conajmniej jedna wartosc jest taka sama {0}.", boat.Name));

            Boolean XAllSame = boat.Points.TrueForAll((point) => point.Location.X.Equals(boat.Points[0].Location.X));

            if (XAllSame)
                return;

            Boolean YAllSame = boat.Points.TrueForAll((point) => point.Location.Y.Equals(boat.Points[0].Location.Y));

            if (!YAllSame)
                throw new Exception(String.Format("{0} Nie moze byc po przekatnej.", boat.Name));

            Boat OverlapBoat = _Boats.Find((aBoat) => aBoat.PointsOverlap(boat));

            if (OverlapBoat != null)
                throw new Exception(String.Format("{0} zachodzi na inny {1}.", boat.Name, OverlapBoat.Name));
        }


        public void DrawGrid(Boolean playerView)
        {
            _Grid.DrawGrid(playerView);
        }

        public Boolean IsHit(Point point)
        {
            foreach (Boat Boat in _Boats)
            {
                if (Boat.IsHit(point))
                {
                    _Grid.SetPin(point, GridPoint.Hit);

                    if (Boat.IsSunk)
                        Console.WriteLine("Zatopiles {0}!", Boat.Name);
                    else
                        Console.WriteLine("Trafiony! ZATOPIONY!!");

                    Console.Beep(1500, 1000);
                    return true;
                }
            }

            Console.WriteLine("Ops, Nie trafiles!");
            Console.Beep(500, 1000);
            _Grid.SetPin(point, GridPoint.Miss);
            return false;
        }
    }
}
