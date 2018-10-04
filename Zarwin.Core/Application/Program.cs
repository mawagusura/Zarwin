using System.Collections.Generic;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Engine
{
    class Program
    {

        public static int Main()
        {
            Simulator sim = new Simulator(true);
            List<SoldierParameters> soldiers=new List<SoldierParameters>();
            for(int i = 1; i <= 10; i++)
            {
                soldiers.Add(new SoldierParameters(i, 1));
            }

            /*
             * Run a simulation with:
             * 100 waves
             * 10 zombies per wave
             * 10 HP in the city
             * 10 soldiers at level 1
             */
            sim.Run(new Parameters(100,new DamageDispatcher(new ItemSelector()),new HordeParameters(10),new CityParameters(10), soldiers.ToArray()));

            return 0;
        }
    }
}
