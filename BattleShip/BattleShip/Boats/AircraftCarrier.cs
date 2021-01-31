using System;

namespace BattleShip.Boats
{
    public class AircraftCarrier : Boat
    {
        public override String Name { get { return "Aircraft Carrier"; } }
        public override Int32 HitsAllowed { get { return 5; } }
    }
}
 