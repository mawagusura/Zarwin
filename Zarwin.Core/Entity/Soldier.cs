using System;
using Zarwin.Core.Exceptions;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity
{
    public class Soldier : ISoldier
    {
        // auto-increment id
        static int nextId = 1;

        public int Id { get; private set; }

        public int HealthPoints { get; set; }

        public int Level { get; private set; } = 1;

        private int MaxHealthPoints => 3 + Level;

        //Attack points start from 1 and up by 1 every 10 level
        //(lvl:11 = 2d, lvl:21 = 3d ...)
        public int AttackPoints => (int)  (1 + Math.Floor((decimal) (Level - 1) / 10));

        public Soldier()
        {
            Id = nextId++;
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
            if(HealthPoints < MaxHealthPoints) HealthPoints++;
        }

        /// <summary>
        /// Raise the level n times
        /// </summary>
        /// <param name="levelToUp"></param>
        public void LevelUp(int levelToUp)
        {
            for(int i = 0; i < levelToUp; i++)
            {
                this.LevelUp();
            }
        }
    }
}
