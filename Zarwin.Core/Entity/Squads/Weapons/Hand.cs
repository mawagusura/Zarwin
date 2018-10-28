using Zarwin.Core.Entity.Cities;

namespace Zarwin.Core.Entity.Squads.Weapons
{
    public class Hand : Weapon 
    {
        public Hand(Wall wall) : base(wall) { }

        public override int AttackMultiplier()
        {
            return 1;
        }
    }
}
