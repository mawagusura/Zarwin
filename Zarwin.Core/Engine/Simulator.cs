using System;
using System.Collections.Generic;
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
            
            //Run a number of run based on the parameter
            for (int i = 0; i < parameters.WavesToRun; i++)
            {
                Printer.PrintMessage("Wave n° " + i);
                wave = new Wave(parameters.HordeParameters, city, parameters.DamageDispatcher, this.Player);

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
