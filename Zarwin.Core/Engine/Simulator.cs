using System.Collections.Generic;
using System.Diagnostics;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    public class Simulator : IInstantSimulator
    {
        public Result Run(Parameters parameters)
        {
            City city = new City(parameters.CityParameters, new List<SoldierParameters>(parameters.SoldierParameters));
            List<WaveResult> waveResults = new List<WaveResult>();
            Wave wave=null;
            for (int i = 0; i < parameters.WavesToRun; i++)
            {
                wave = new Wave(parameters.HordeParameters, city, parameters.DamageDispatcher, false);
                if (wave.City.GameOver())
                {
                    waveResults.Add(wave.WaveResult());
                    break;
                }
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
