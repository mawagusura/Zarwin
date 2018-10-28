using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity.Cities;
using Zarwin.Core.Entity.Waves;
using Zarwin.Shared.Contracts;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    public class Simulator : IInstantSimulator
    {
        public City City { get; private set; }
        public UserInterface UserInterface { get; }

        private readonly List<WaveResult> waveResults = new List<WaveResult>();

        public Simulator(Boolean player)
        {
            this.UserInterface = new UserInterface(player);
        }
        public void InitializeSimulator(Parameters parameters)
        {
            this.City = new City(parameters.CityParameters,parameters.SoldierParameters,this.UserInterface);
        }

        private List<Order> CurrentOrders(Order[] orders,int wave)
            => orders.Where(order => order.WaveIndex == wave).ToList();

        private ZombieParameter[] CurrentWaveZombieParameters(HordeParameters hordeParameters, int wave)
            => hordeParameters.Waves[wave % hordeParameters.Waves.Length].ZombieTypes;

        public Result Run(Parameters parameters)
        { 
            this.InitializeSimulator(parameters);
            int i = 0;
            WaveResult waveResult;
            do
            {
                waveResult=this.RunWave(CurrentWaveZombieParameters(parameters.HordeParameters,i),
                      CurrentOrders(parameters.Orders,i),parameters.DamageDispatcher);

                this.waveResults.Add(waveResult);
                this.UserInterface.InvokeEndWave();
                i++;
            }
            while (i < parameters.WavesToRun && this.City.Squad.IsAlive);
               
            return new Result(waveResults.ToArray());
        }

        private WaveResult RunWave(ZombieParameter[] zombieParameters,List<Order> orders,IDamageDispatcher damageDispatcher)
        {
            Wave currentWave = new Wave(zombieParameters, this.City,orders,damageDispatcher);
            this.City.OrderHandler.ExecuteOrders();

            if (this.City.Squad.IsAlive)
            {
                currentWave.Run();
            }
            return currentWave.WaveResult;
        }
        
    }
}
