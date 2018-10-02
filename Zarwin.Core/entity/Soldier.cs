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

        private int MaxHealthPoints { get; set; } = 3;

        public int Id { get; private set; }

        public int HealthPoints { get; set; }

        public int Level { get; private set; } = 1;

        public Boolean Alive = true;

        public Soldier()
        {
            Id = nextId++;
            HealthPoints = MaxHealthPoints;
        }

        /// <summary>
        /// Decrease the HealthPoints by "damage" value
        /// </summary>
        /// <param name="damage"></param>
        public void Hurt(int damage)
        {
            if(damage > HealthPoints)
            {
                this.Alive = false;
                this.HealthPoints = 0;
            }
            else
            {
                HealthPoints -= damage;
            }
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
            MaxHealthPoints += Level;
        }
    }
}
