using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    public class Simulator : IInstantSimulator
    {
        private Boolean Player { get; }

        public Simulator(Boolean player)
        {
            this.Player = player;
            Soldier.NextId = 1;
        }

        /// <summary>
        /// Run a simulation of the game
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Result Run(Parameters parameters)
        {
            City city = new City(parameters.CityParameters, new List<SoldierParameters>(parameters.SoldierParameters));
            List<WaveResult> waveResults = new List<WaveResult>();
            Wave wave=null;
            List<Order> orders = new List<Order>(parameters.Orders);
            List<Order> currentOrders= new List<Order>();
            //Run a number of run based on the parameter
            for (int i = 0; i < parameters.WavesToRun; i++)
            {
                currentOrders.Clear();
                IEnumerable<Order> res = orders.Where(order => order.WaveIndex == i);
                if (res.Any())
                {
                    currentOrders.AddRange(res.ToList());
                }
                

                UserInterface.PrintMessage("Wave n° " + i,Player);

                if (parameters.HordeParameters.Waves.Length <= i)
                {
                    wave = new Wave(parameters.HordeParameters.Waves[0],city, parameters.DamageDispatcher,this.Player, currentOrders);
                }
                else
                {
                    wave = new Wave(parameters.HordeParameters.Waves[i], city, parameters.DamageDispatcher,
                        this.Player, currentOrders);
                }
                //Exist the game when the game is over
                if (wave.City.GameOver())
                {
                    waveResults.Add(wave.WaveResult());
                    break;
                }

                //Run a wave
                else
                {
                    wave.Run();
                    waveResults.Add(wave.WaveResult());
                }
            }
            return new Result(waveResults.ToArray());
        }
        
    }
}
