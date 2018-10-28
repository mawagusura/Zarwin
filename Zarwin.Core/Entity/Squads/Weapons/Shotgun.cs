using Zarwin.Core.Entity.Cities;

namespace Zarwin.Core.Entity.Squads.Weapons
{
    public class Shotgun : Weapon
    {
        public Shotgun(Wall wall) : base(wall) { }

        public override int AttackMultiplier()
        {
            return (wall.HealthPoints == 0) ? 2 : 1;   
        }
    }
}
