using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity.Cities;
using Zarwin.Core.Entity.Squads;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity.Waves
{
    public class Horde
    {
        private City City { get; }
        private List<Zombie> Zombies { get; } = new List<Zombie>();
        private readonly int zombieId = 1;

        public List<Zombie> ZombiesAlive
            => Zombies.Where(z => z.HealthPoints > 0).ToList();
        public HordeState HordeState
            => new HordeState(ZombiesAlive.Count);
        public Boolean IsAlive
            => this.ZombiesAlive.Count > 0;
        
        public Horde(ZombieParameter[] zombieParameters) : this(zombieParameters, new City()) { }

        public Horde(ZombieParameter[] zombieParameters, City city)
        {
            foreach (ZombieParameter z in zombieParameters)
            {
                for (int i = 0; i < z.Count; i++)
                {
                    this.Zombies.Add(new Zombie(this.zombieId++,z.Trait,z.Type));
                }
            }
            // Sort zombies
            this.Zombies.Sort();
            this.City = city;
        }

        /// <summary>
        /// A soldier kill zombies based on it attack and the number of zombies still "alive"
        /// The soldier levelup foreach zombie killed
        /// </summary>
        /// <param name="soldier"></param>
        public int AttackZombies(Soldier soldier, int turn)
        {
            int attack = soldier.AttackPoints;
            int zombieKilled = 0;

            int healthPoints;
            Zombie zombie;
            while (attack > 0 && this.IsAlive)
            {
                zombie= ZombiesAlive[0];
                healthPoints = zombie.HealthPoints;
                zombie.Hurt(attack, turn);
                attack -= healthPoints;

                if (zombie.HealthPoints == 0)
                {
                    soldier.LevelUp();
                    zombieKilled++;
                }
            }
            this.City.UserInterface.InvokeSoliderKill(soldier.Id, zombieKilled);
            return zombieKilled;
        }
    }

    
}
