using System;
using Zarwin.Core.Entity.Soldiers.Weapons;
using Zarwin.Core.Exceptions;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity.Soldiers
{
    public class Soldier : ISoldier
    {
        // auto-increment id
        public static int NextId { get; private set; } = 1;

        public int Id { get; private set; }

        public int HealthPoints { get; private set; }

        public int Level { get; private set; } = 1;

        public static void InitId()
        {
            Soldier.NextId = 1;
        }

        public Weapon Weapon { get; private set; } = new Hand(null);

        private int MaxHealthPoints => 3 + Level;

        public void SetWeapon(Weapon weapon)
        {
            if(this.Weapon is Hand)
            {
                this.Weapon = weapon;
            }
        }

        //Attack points start from 1 and up by 1 every 10 level
        //(lvl:11 = 2d, lvl:21 = 3d ...)
        public int AttackPoints
            => (1 +(Level - 1) / 10)* this.Weapon.AttackMultiplier();

        public Soldier()
        {
            Id = NextId++;
            HealthPoints = MaxHealthPoints;
        }

        /// <summary>
        /// Create a soldier based on parameter
        /// </summary>
        /// <param name="soldierParameters"></param>
        public Soldier(SoldierParameters soldierParameters)
        {
            if (soldierParameters.Id < 0 || soldierParameters.Level < 1) throw new WrongParameterException("Parameters with wrong values");

            Id = soldierParameters.Id;
            Level = soldierParameters.Level;
            HealthPoints = MaxHealthPoints;
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
