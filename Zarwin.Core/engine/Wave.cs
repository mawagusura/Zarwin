using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    public class Wave
    {
        private List<Zombie> zombies;
        private readonly City city;

        private TurnResult InitialResult { get; }
        private List<TurnResult> TurnResults { get; }

        private readonly Boolean console;


        public Wave(HordeParameters hordeParameter, City city, Boolean console)
        {
            this.zombies = Enumerable.Repeat(new Zombie(), hordeParameter.Size).ToList();
            this.city = city;
            this.console = console;

            //Create InitialResult
            this.InitialResult = new TurnResult(city.GetSoldierStates().ToArray(), new HordeState(zombies.Count()), city.WallHealthPoints);
        }

        public void Run()
        {
            //Beginning of the wave
            this.BeginningWave();

            int i = 1;
            Turn currentRound;
            //Rounds
            while (this.zombies.size() > 0)
            {
                Console.WriteLine("Round {0}", i);

                currentRound = this.rounds.Dequeue();
                currentRound.Run();

                Console.ReadLine();
            }
            
        }

        
        public 

        private HordeState CreateHordeState()
        {
            return new HordeState(this.zombies.Count());
        }
    }
}