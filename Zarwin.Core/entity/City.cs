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
        private readonly int baseWallHealthPoints = 10;

        public int WallHealthPoints { get; set; }

        public List<Soldier> Soldiers { get; private set; }

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
        /// Constructor of the City class
        /// </summary>
        /// <param name="soldierParameters"></param>
        public City(List<SoldierParameters> soldierParameters)
        {
            WallHealthPoints = baseWallHealthPoints;

            // initilaize Soldiers with parameters
            soldierParameters.ForEach(sp => Soldiers.Add(new Soldier(sp.Id, sp.Level)));

        }

        public void HurtWall(int amount)
        {
            WallHealthPoints= amount > WallHealthPoints ?  0 : WallHealthPoints - amount;
        }

    }
}
