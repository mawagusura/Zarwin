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
            
            for (int i = 0; i < parameters.WavesToRun; i++)
            {
                waveResults.Add(new Wave(parameters.HordeParameters, city, parameters.DamageDispatcher, false).Run());
            }
            Debug.WriteLine(new Result(waveResults.ToArray()));
            return new Result(waveResults.ToArray());
        }
    }
}
