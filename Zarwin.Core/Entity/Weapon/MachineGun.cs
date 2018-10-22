
namespace Zarwin.Core.Entity.Weapon
{
    public class MachineGun : IWeapon
    {
        public int AttackMultiplier(City city)
        {
            return (city.WallHealthPoints > 0) ? 4 : 1;
        }
    }
}
