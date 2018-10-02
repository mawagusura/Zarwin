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
        private Queue<Turn> rounds;

        private readonly TurnResult initialResult;
        private TurnResult[] turnsResult;

        private readonly Boolean console;


        public Wave(HordeParameters hordeParameter, City city, Boolean console)
        {
            this.zombies = Enumerable.Repeat(new Zombie(), hordeParameter.Size).ToList();
            this.city = city;
            this.rounds = new Queue<Turn>();
            this.console = console;

            //Create InitialResult
<<<<<<< Updated upstream
            this.initialResult = new TurnResult(city.GetSoldierStates().ToArray(), new HordeState(zombies.Count()), city.WallHealthPoints);
=======
            this.initialResult = new TurnResult(city.SoldierState.ToArray(),new HordeState(zombies.Count()),city.WallHealthPoints);
>>>>>>> Stashed changes

            //Add rounds
            this.rounds.Enqueue(new WarnRound());

            this.AddSoldiersZombieRound();

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

            //End of the wave
            this.EndWave();
        }


        private void BeginningWave()
        {
            Console.WriteLine("Beginning of the wave");
            Console.ReadLine();
        }

        private void EndWave()
        {
            Console.WriteLine("End of the wave");
            Console.ReadLine();
        }

        public void AddRound(List<Turn> newRounds)
        {
            foreach (Turn round in newRounds)
            {
                this.rounds.Push(round);
            }
        }

        public void AddSoldiersZombieRound()
        {
            foreach (Soldier sold in city.GetSoldierAlive())
            {
                this.rounds.Enqueue(new SoldierRound(sold));
            }
            this.rounds.Enqueue(new ZombieRound());
        }

        private HordeState CreateHordeState()
        {
            return new HordeState(this.zombies.Count());
        }
    }
}