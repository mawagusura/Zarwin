using System.Collections.Generic;
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

            WaveResult wave=null;
            for (int i = 0; i < parameters.WavesToRun; i++)
            {
                wave = new Wave(parameters.HordeParameters, city, parameters.DamageDispatcher, false).Run();
            }
                waveResults.Add(wave);
            return new Result(waveResults.ToArray());
        }
    }
}
