using System;
using System.Drawing;

namespace BattleShip
{
    public class HitPoint
    {

        public Point Location { get; set; }


        public Boolean Hit { get; set; }

        public override Boolean Equals(Object point)
        {
            if (null == point || !(point is HitPoint))
                return false;

            HitPoint OtherPoint = point as HitPoint;
            return OtherPoint.Location.Equals(Location);
        }

        public override Int32 GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}
