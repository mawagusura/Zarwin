using System;
using System.Collections.Generic;
using System.Text;
using Zarwin.Shared.Contracts.Core;

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

        public Soldier(int id, int level)
        {
            if (id < 0 || level < 1) throw new Exception("Parameters with wrong values");

            Id = id;
            Level = level;            
        }

        /// <summary>
        /// Decrease the HealthPoints by "damage" value
        /// </summary>
        /// <param name="damage"></param>
        public void Hurt(int damage)
        {
            if(damage > HealthPoints) this.HealthPoints = 0;
            else HealthPoints -= damage;
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
