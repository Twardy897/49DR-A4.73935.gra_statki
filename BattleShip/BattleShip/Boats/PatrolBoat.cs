using System;

namespace BattleShip.Boats
{
    public class PatrolBoat : Boat
    {
        public override String Name { get { return "Patrol Boat"; } }
        public override Int32 HitsAllowed { get { return 2; } }
    }
}
