using System;

namespace BattleShip.Boats
{
    public class Battleship : Boat
    {
        public override String Name { get { return "Battleship"; } }
        public override Int32 HitsAllowed { get { return 4; } }
    }
}
