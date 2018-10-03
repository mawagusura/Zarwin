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
        public List<Zombie> Zombies { get; }
        public City City { get; }
        private readonly Boolean waiting;


        public IDamageDispatcher Dispatcher { get; }
        private TurnResult InitialResult { get; }
        private List<TurnResult> TurnResults { get; }
        private Queue<Turn> turns;


        public Wave(HordeParameters hordeParameter, City city, IDamageDispatcher dispatcher, Boolean waiting)
        {
            this.Zombies = Enumerable.Repeat(new Zombie(), hordeParameter.Size).ToList();
            this.City = city;
            this.waiting = waiting;
            this.Dispatcher = dispatcher;

            this.turns.Enqueue(new ApproachTurn(this));
            //Create InitialResult
            this.InitialResult = new TurnResult(city.SoldierState.ToArray(), new HordeState(Zombies.Count()), city.WallHealthPoints);

        }


        public WaveResult Run()
        {
            Turn currentTurn=this.turns.Dequeue();
            TurnResult tmpTurnResult;

            while (this.Zombies.Count() > 0 && this.City.Soldiers.Sum(soldier => soldier.HealthPoints) > 0)
            {
                if (currentTurn.Equals(null))
                {
                    //It should exit the loop before dequeue a null
                    throw new UnreachableCodeException("Win condition failed, dequeue null turn");
                }

                tmpTurnResult=currentTurn.Run();
                if (tmpTurnResult == null)
                {
                    //The wave is over, all zombies died or all soldiers
                    break;
                }

                //Save the turn
                this.TurnResults.Add(tmpTurnResult);

                currentTurn = this.turns.Dequeue();
            }
            return new WaveResult(this.InitialResult,this.TurnResults.ToArray());
        }

        public void EnqueueCompleteRound()
        {
            foreach (Soldier soldier in this.City.Soldiers)
            {
                if (soldier.HealthPoints > 0)
                {
                    this.turns.Enqueue(new SoldierTurn(this, soldier));
                }
            }

            this.turns.Enqueue(new ZombieTurn(this));
        }

        //States 
        private HordeState HordeState() => new HordeState(this.Zombies.Count());

        public TurnResult CurrentTurnResult()
           => new TurnResult(this.City.SoldierState.ToArray(), this.HordeState(), this.City.WallHealthPoints);

        public void WaitPlayer()
        {
            if (this.waiting)
            {
                Console.ReadLine();
            }
        }
    }
}