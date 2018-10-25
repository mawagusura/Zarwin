using Zarwin.Core.Entity.Weapon;
using Zarwin.Core.Exceptions;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity
{
    public class Soldier : ISoldier
    {
        // auto-increment id
        public static int NextId { get; set; } = 1;

        public int Id { get; private set; }

        public int HealthPoints { get; private set; }

        public int Level { get; private set; } = 1;

        public IWeapon Weapon { get; set; } = new Hand();

        private int MaxHealthPoints => 3 + Level;

        private readonly City city;

        //Attack points start from 1 and up by 1 every 10 level
        //(lvl:11 = 2d, lvl:21 = 3d ...)
        public int AttackPoints
            => (1 +(Level - 1) / 10)* this.Weapon.AttackMultiplier(city);

        public Soldier(City city)
        {
            Id = NextId++;
            HealthPoints = MaxHealthPoints;
            this.city = city;
        }

        /// <summary>
        /// Create a soldier based on parameter
        /// </summary>
        /// <param name="soldierParameters"></param>
        public Soldier(SoldierParameters soldierParameters,City city)
        {
            if (soldierParameters.Id < 0 || soldierParameters.Level < 1) throw new WrongParameterException("Parameters with wrong values");

            Id = soldierParameters.Id;
            Level = soldierParameters.Level;
            HealthPoints = MaxHealthPoints;
            this.city = city;
            NextId++;
        }

        /// <summary>
        /// Decrease the HealthPoints by "damage" value
        /// </summary>
        /// <param name="damage"></param>
        public void Hurt(int damage)
        {
            HealthPoints = damage > HealthPoints ? 0 : HealthPoints - damage;
        }

        /// <summary>
        /// Raise the level of the soldier which includes :
        ///     * +1 health point
        ///     *  maxHealthPoint raised by Level value
        /// </summary>
        public void LevelUp()
        {
            Level++;
            if (HealthPoints < MaxHealthPoints)
            {
                HealthPoints++;
            }
        }

    }
}
