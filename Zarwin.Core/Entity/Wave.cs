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

            //Create WaveResult
            return new WaveResult(this.InitialResult,this.TurnResults.ToArray());
        }

        //States
        private HordeState HordeState() => new HordeState(this.zombies.Count());

        private TurnResult CurrentTurnResult()
            => new TurnResult(this.city.SoldierState.ToArray(), this.HordeState(), this.city.WallHealthPoints);


        //Turns

        private void ApproachTurn()
        {
            this.PrintMessage("Horde in approach");
            if (this.console)
            {
                Console.ReadLine();
            }
        }

        private void SoliderTurn(Soldier soldier)
        {
            if(soldier.AttackPoints >= this.zombies.Count)
            {
                this.PrintMessage("Solider " + soldier.Id + " kills " + Math.Min(this.zombies.Count, soldier.AttackPoints) + " zombie(s)");
                this.zombies.Clear();
            }
            else
            {
                this.PrintMessage("Solider " + soldier.Id + "kills " + soldier.AttackPoints + "zombies");
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
                this.PrintMessage("Zombies attack wall");
            }
            else
            {
                this.dispatcher.DispatchDamage(this.zombies.Count,this.city.Soldiers);
                this.PrintMessage("Zombies attack soldiers");
            }

            if (this.console)
            {
                Console.ReadLine();
            }
        }

        

        //Centralise interaction with the view
        private void PrintMessage(String message)
        {
            Console.WriteLine(message);
        }

    }
}