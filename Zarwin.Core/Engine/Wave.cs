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
        public int Zombies { get; set; }
        public City City { get; }
        private readonly Boolean waiting;


        public IDamageDispatcher Dispatcher { get; }
        private TurnResult InitialResult { get; }
        private List<TurnResult> TurnResults { get; }

        private readonly Queue<Turn> turns;


        public Wave(HordeParameters hordeParameter, City city, IDamageDispatcher dispatcher, Boolean waiting)
        {
            this.Zombies = hordeParameter.Size;
            this.City = city;
            this.waiting = waiting;
            this.Dispatcher = dispatcher;
            this.turns = new Queue<Turn>();

            this.turns.Enqueue(new ApproachTurn(this));
            //Create InitialResult
            this.InitialResult = new TurnResult(city.SoldierState.ToArray(), new HordeState(Zombies), city.WallHealthPoints);

        }


        public WaveResult Run()
        {
            Turn currentTurn=this.turns.Dequeue();

            while (!this.WaveOver() && !this.City.GameOver())
            {
                if (currentTurn.Equals(null))
                {
                    //It should exit the loop before dequeue a null
                    throw new UnreachableCodeException("Win condition failed, dequeue null turn");
                }

                //Save the turn
                this.TurnResults.Add(currentTurn.Run());

                currentTurn = this.turns.Dequeue();
            }
            return new WaveResult(this.InitialResult,this.TurnResults.ToArray());
        }

        public Boolean WaveOver() => this.Zombies == 0;

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

        public void KillZombie(int attackPoints)
        {
            if (this.Zombies < attackPoints)
            {
                this.Zombies = 0;
            }
            else
            {
                this.Zombies -= attackPoints;
            }
        }

        //States 
        private HordeState HordeState() => new HordeState(this.Zombies);

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