using Zarwin.Core.Entity.Cities;

namespace Zarwin.Core.Entity.Soldiers.Weapons
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
