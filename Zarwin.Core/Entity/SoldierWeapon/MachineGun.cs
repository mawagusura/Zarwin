
namespace Zarwin.Core.Entity.SoldierWeapon
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
