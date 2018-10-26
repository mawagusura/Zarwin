using Zarwin.Core.Entity.Cities;

namespace Zarwin.Core.Entity.Soldiers.Weapons
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
