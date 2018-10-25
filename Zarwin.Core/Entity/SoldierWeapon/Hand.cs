
namespace Zarwin.Core.Entity.SoldierWeapon
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
