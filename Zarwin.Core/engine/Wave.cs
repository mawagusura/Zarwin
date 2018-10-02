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
        private HordeState HordeState() => new HordeState(this.zombies.Count());

        private readonly Boolean console;


        public Wave(HordeParameters hordeParameter, City city, Boolean console)
        {
            this.zombies = Enumerable.Repeat(new Zombie(), hordeParameter.Size).ToList();
            this.city = city;
            this.console = console;

            //Create InitialResult
            this.InitialResult = new TurnResult(city.SoldierState.ToArray(), new HordeState(zombies.Count()), city.WallHealthPoints);
        }

        public void Run()
        {
            //ApproachTurn
            ApproachTurn();
            this.TurnResults.Add(this.CurrentTurnResult());

            //Rounds
            while (this.zombies.Count() > 0)
            {
                
                if (this.console)
                {
                    Console.ReadLine();
                }
                
            }
            
        }
        
        private TurnResult CurrentTurnResult()
            => new TurnResult(this.city.SoldierState.ToArray(), this.HordeState(), this.city.WallHealthPoints);

        private void ApproachTurn()
        {
            Console.WriteLine("Horde in approach");
            if (this.console)
            {
                Console.ReadLine();
            }
        }

    }
}