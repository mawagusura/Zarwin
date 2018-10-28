using System;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity.Waves
{
    public class Zombie : IComparable<Zombie>
    {
        public ZombieTrait Trait { get; }

        public ZombieType Type { get; }

        public int HealthPoints { get; set; } = 1;

        private int LastTurn = -1;

        public int Id { get; }
        

        public Zombie(int id,ZombieTrait zombieTrait,ZombieType zombieType)
        {
            this.Trait = zombieTrait;
            this.Type = zombieType;
            this.Id=id;
        }

        /// <summary> 
        /// Hurt the zombie 
        /// </summary> 
        /// <param name="dmg"></param> 
        /// <param name="turn"></param> 
        public void Hurt(int dmg, int turn)
        {
            // on tape si c'est du normal ou si on a déjà tapé ce tour
            if(Trait != ZombieTrait.Tough || LastTurn==turn)
            {
                HealthPoints = dmg > HealthPoints ? 0 : HealthPoints - dmg;
            }  
            //sinon on icrémente le tour
            else if (Trait == ZombieTrait.Tough) LastTurn = turn;
        }

        /// <summary> 
        /// Compate two zombie 
        /// </summary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(Zombie other)
        {
            if (other.Type != Type) return Type-other.Type;

            if (other.Trait != Trait) return Trait-other.Trait;

            return 0;
        }
    }
}
