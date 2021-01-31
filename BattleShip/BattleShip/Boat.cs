using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BattleShip
{
    public class Boat
    {
        public virtual String Name { get { return null; } }

        public virtual Int32 HitsAllowed { get { return 0; } }

        public Boolean IsSunk { get { return Points.All((point) => point.Hit); } }
        public List<HitPoint> Points { get; set; }

        public Boolean PointsOverlap(Boat otherBoat)
        {
            return this.Points.Any((point) => otherBoat.Points.Any((otherPoint) => otherPoint.Equals(point)));
        }
        public Boolean IsHit(Point point)
        {
            if (null == Points)
                throw new Exception("Boat is not setup.");

            HitPoint HitPoint = Points.FirstOrDefault((aPoint) => { return (aPoint.Location.X == point.X && aPoint.Location.Y == point.Y); });

            if (HitPoint != null)
            {
                if (HitPoint.Hit)
                    throw new Exception("Point was already hit.");

                HitPoint.Hit = true;
                return true;
            }

            return false;
        }
    }
}
