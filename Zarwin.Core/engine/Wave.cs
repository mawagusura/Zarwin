using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Core;
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
        private readonly IDamageDispatcher dispatcher;

        public Wave(HordeParameters hordeParameter, City city, IDamageDispatcher dispatcher, Boolean console)
        {
            this.zombies = Enumerable.Repeat(new Zombie(), hordeParameter.Size).ToList();
            this.city = city;
            this.console = console;
            this.dispatcher = dispatcher;

            //Create InitialResult
            this.InitialResult = new TurnResult(city.SoldierState.ToArray(), new HordeState(zombies.Count()), city.WallHealthPoints);
        }

        public WaveResult Run()
        {
            //ApproachTurn
            ApproachTurn();
            this.TurnResults.Add(this.CurrentTurnResult());

            //SoldierTurns
            foreach(Soldier soldier in this.city.Soldiers)
            {
                if (this.zombies.Count() > 0 || soldier.HealthPoints == 0)
                {
                    break;
                }

                this.SoliderTurn(soldier);
                this.TurnResults.Add(this.CurrentTurnResult());
            }

            //ZombieTurn
            this.ZombieTurn();
            this.TurnResults.Add(this.CurrentTurnResult());

            return new WaveResult(this.InitialResult,this.TurnResults.ToArray());
        }
        
        private TurnResult CurrentTurnResult()
            => new TurnResult(this.city.SoldierState.ToArray(), this.HordeState(), this.city.WallHealthPoints);

        private void SoliderTurn(Soldier soldier)
        {
            if(soldier.AttackPoints >= this.zombies.Count)
            {
                this.zombies.Clear();
            }
            else
            {
                for (int i = 0; i < soldier.AttackPoints; i++)
                {
                    this.zombies.RemoveAt(0);
                }
            }

            if (this.console)
            {
                Console.ReadLine();
            }
        }

        private void ZombieTurn()
        {
            if (this.city.WallHealthPoints > 0)
            {
                this.city.HurtWall(this.zombies.Count);
                Console.WriteLine("Zombies attack wall");
            }
            else
            {
                this.dispatcher.DispatchDamage(this.zombies.Count,this.city.Soldiers);
                Console.WriteLine("Zombies attack soldiers");
            }

            if (this.console)
            {
                Console.ReadLine();
            }
        }

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