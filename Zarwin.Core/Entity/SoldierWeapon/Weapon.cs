
namespace Zarwin.Core.Entity.SoldierWeapon
{
    public abstract class Weapon 
    {
        protected Wall wall;
        public Weapon(Wall wall)
        {
            this.wall = wall;
        }
        public abstract int AttackMultiplier();
    }
}
