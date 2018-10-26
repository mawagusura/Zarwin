using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity.Soldiers;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity.Zombies
{
    public class Horde
    {

        private List<Zombie> zombies = new List<Zombie>();
        public List<Zombie> ZombiesAlive => zombies.Where(z => z.HealthPoints > 0).ToList();

        public Horde(ZombieParameter[] zombieParameters)
        {
            foreach (ZombieParameter z in zombieParameters)
            {
                for (int i = 0; i < z.Count; i++)
                {
                    zombies.Add(new Zombie(z));
                }
            }
            // Sort zombies
            zombies.Sort();
        }

        /// <summary>
        /// Create an HordeState of the current situation
        /// </summary>
        /// <returns></returns>
        public HordeState HordeState 
            => new HordeState(ZombiesAlive.Count);

        public Boolean IsAlive()
            => this.ZombiesAlive.Count == 0;

        /// <summary>
        /// A soldier kill zombies based on it attack and the number of zombies still "alive"
        /// The soldier levelup foreach zombie killed
        /// </summary>
        /// <param name="soldier"></param>
        public int AttackZombies(Soldier soldier, int turn)
        {
            zombies.Sort();
            int attack = soldier.AttackPoints;
            int zombieKilled = 0;
            while (attack > 0 && !this.IsAlive())
            {
                Zombie temp = ZombiesAlive[0];
                int def = temp.HealthPoints;
                temp.Hurt(attack, turn);
                attack -= def;
                if (temp.HealthPoints == 0)
                {
                    soldier.LevelUp();
                    zombieKilled++;
                  //  this.city.IncreaseMoney(1);
                }
            }
            return zombieKilled;
        }
    }

    
}
