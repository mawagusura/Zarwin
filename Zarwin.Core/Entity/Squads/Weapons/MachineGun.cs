using Zarwin.Core.Entity.Cities;

namespace Zarwin.Core.Entity.Squads.Weapons
{
    public class MachineGun : Weapon
    {
        public MachineGun(Wall wall): base(wall){}

        public override int AttackMultiplier()
        {
            return (wall.HealthPoints> 0) ? 4 : 1;
        }
    }
}
