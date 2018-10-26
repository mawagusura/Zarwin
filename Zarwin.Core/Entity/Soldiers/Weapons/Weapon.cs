using Zarwin.Core.Entity.Cities;

namespace Zarwin.Core.Entity.Soldiers.Weapons
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
