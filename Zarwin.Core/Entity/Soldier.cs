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

        public int AttackPoints => (int)  (1 + Math.Floor((decimal) (Level - 1) / 10));

        public Soldier()
        {
            Id = nextId++;
            HealthPoints = MaxHealthPoints;
        }

        public Soldier(SoldierParameters soldierParameters)
        {
            if (soldierParameters.Id < 0 || soldierParameters.Level < 1) throw new WrongParameterException("Parameters with wrong values");

            Id = soldierParameters.Id;
            Level = soldierParameters.Level;            
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
    }
}
