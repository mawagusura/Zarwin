using System;
using System.Collections.Generic;
using System.Text;

namespace Zarwin.Core.entity
{
    public class City
    {
        private int baseWallHealthPoints = 10;

        public int WallHealthPoints { get; set; }

        public List<Soldier> soldiers { get; private set; }

        public City()
        {
            WallHealthPoints = baseWallHealthPoints;
        }
    }
}
