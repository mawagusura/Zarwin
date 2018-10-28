using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Zarwin.Core.Engine;
using Zarwin.Core.Engine.Tool;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Application
{
    class Program
    {
        [ExcludeFromCodeCoverage]
        public static int Main()
        {
            Simulator sim = new Simulator(true);
            List<SoldierParameters> soldiers=new List<SoldierParameters>();
            for(int i = 1; i <= 2; i++)
            {
                soldiers.Add(new SoldierParameters(i, 1));
            }


            /*
             * Run a simulation with:
             * 5 waves
             * 5 zombies per wave
             * 3 HP in the city
             * 2 soldiers at level 1
             */
            sim.Run(new Parameters(5,new DamageDispatcher(new ItemSelector()),new HordeParameters(5),new CityParameters(3),new Order[1], soldiers.ToArray()));
            
            return 0;
        }
    }
}
