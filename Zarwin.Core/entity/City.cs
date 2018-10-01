using System;
using System.Collections.Generic;
using System.Text;
using Zarwin.Shared.Contracts.Core;

namespace Zarwin.Core.Entity
{
    public class City
    {
        private int baseWallHealthPoints = 10;

        public int WallHealthPoints { get; set; }

        public List<ISoldier> soldiers { get; private set; }

        public City()
        {
            WallHealthPoints = baseWallHealthPoints;
        }
    }
}
