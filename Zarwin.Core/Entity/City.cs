using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity
{
    public class City
    {
        public int WallHealthPoints { get; set; } = 10;

        public List<Soldier> Soldiers { get; private set; } = new List<Soldier>();

        public List<Soldier> AliveSoldiers => Soldiers.Where(Alive => true).ToList();

        public List<SoldierState> SoldierState
        {
            get
            {
                List<SoldierState> states = new List<SoldierState>();
                Soldiers.ForEach(sold => states.Add(new SoldierState(sold.Id, sold.Level, sold.HealthPoints)));
                return states;
            }
        }

        /// <summary>
        ///Empty constructor for unit tests
        /// </summary>
        public City()
        {
        }

        /// <summary>
        /// Constructor of the City class
        /// </summary>
        /// <param name="soldierParameters"></param>
        public City(CityParameters cityParameter ,List<SoldierParameters> soldierParameters)
        {
            WallHealthPoints = cityParameter.WallHealthPoints;
            // initilaize Soldiers with parameters
            soldierParameters.ForEach(sp => Soldiers.Add(new Soldier(sp)));
        }

        public void HurtWall(int amount)
        {
            WallHealthPoints= amount > WallHealthPoints ?  0 : WallHealthPoints - amount;
        }

        /// <summary>
        /// Test is the game is over, there is no soldier or there are all dead
        /// </summary>
        /// <returns></returns>
        public Boolean GameOver()
        {
            return (this.Soldiers.Sum(soldier => soldier.HealthPoints) == 0) || (this.Soldiers.Count==0);
        }
    }
}
