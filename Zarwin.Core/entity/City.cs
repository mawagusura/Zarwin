using System;
using System.Collections.Generic;
using System.Text;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity
{
    public class City
    {
        private readonly int baseWallHealthPoints = 10;

        public int WallHealthPoints { get; set; }

        public List<ISoldier> Soldiers { get; private set; }
        public City()
        {
            WallHealthPoints = baseWallHealthPoints;
        }

        public List<SoldierState> GetSoldierStates()
        {
            List<SoldierState> states = new List<SoldierState>();
            foreach (ISoldier sold in Soldiers)
            {
                states.Add(new SoldierState(sold.Id, sold.Level, sold.HealthPoints));
            }
            return states;
        }

    }
}
