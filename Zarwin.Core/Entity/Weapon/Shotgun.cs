
namespace Zarwin.Core.Entity.Weapon
{
    public class Shotgun : IWeapon
    {
        public int AttackMultiplier(City city)
        {
            return (city.WallHealthPoints == 0) ? 2 : 1;   
        }
    }
}
