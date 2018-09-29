using System;
using System.Collections.Generic;
using System.Text;

namespace Zarwin.Core.entity
{
    class City
    {
        public Wall wall { get; set; }

        public List<Soldier> soldiers { get; private set; }

        public City()
        {

        }
    }
}
