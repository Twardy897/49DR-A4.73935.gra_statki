using System;

namespace BattleShip.Boats
{
    public class Destroyer : Boat
    {
        public override String Name { get { return "Destroyer"; } }
        public override Int32 HitsAllowed { get { return 3; } }
    }
}
