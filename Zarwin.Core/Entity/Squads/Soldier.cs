using Zarwin.Core.Entity.Squads.Weapons;
using Zarwin.Core.Exceptions;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity.Squads
{
    public class Soldier : ISoldier
    {
        private int MaxHealthPoints => 3 + Level;

        public int Id { get; private set; }

        public int HealthPoints { get; private set; }

        public int Level { get; private set; } = 1;

        public Squad Squad { get; }

        public Weapon Weapon { get; private set; }
               
        //Attack points start from 1 and up by 1 every 10 level
        //(lvl:11 = 2d, lvl:21 = 3d ...)
        public int AttackPoints
            => (1 +(Level - 1) / 10)* this.Weapon.AttackMultiplier();

        public Soldier(int id, Squad squad) : this(new SoldierParameters(id, 1), squad) { }

        /// <summary>
        /// Create a soldier based on parameter
        /// </summary>
        /// <param name="soldierParameters"></param>
        public Soldier(SoldierParameters soldierParameters,Squad squad)
        {
            if (soldierParameters.Id < 0 || soldierParameters.Level < 1) throw new WrongParameterException("Parameters with wrong values");

            this.Id = soldierParameters.Id;
            this.Level = soldierParameters.Level;
            this.HealthPoints = MaxHealthPoints;
            this.Squad = squad;
            this.Weapon= new Hand(null);
        }

        /// <summary>
        /// Decrease the HealthPoints by "damage" value
        /// </summary>
        /// <param name="damage"></param>
        public void Hurt(int damage)
        {
            if(damage < HealthPoints)
            {
                HealthPoints -= damage;
                this.Squad.City.UserInterface.InvokeSoliderHit(this.Id, damage);
                
            }
            else
            {
                HealthPoints = 0;
                this.Squad.City.UserInterface.InvokeSoliderDown(this.Id);
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
            if (HealthPoints < MaxHealthPoints)
            {
                HealthPoints++;
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            if (this.Weapon is Hand)
            {
                this.Weapon = weapon;
            }
        }
    }
}
