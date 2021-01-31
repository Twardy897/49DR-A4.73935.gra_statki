using System;

namespace BattleShip.Boats
{
    public class Submarine : Boat
    {
        public override String Name { get { return "Submarine"; } }
        public override Int32 HitsAllowed { get { return 3; } }
    }
}
