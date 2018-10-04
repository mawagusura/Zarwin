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
        private TurnResult InitialResult { get; set; }
        private List<TurnResult> TurnResults { get; }

        private readonly Queue<Turn> turns;


        public Wave(HordeParameters hordeParameter, City city, IDamageDispatcher dispatcher, Boolean waiting)
        {
            this.Zombies = hordeParameter.Size;
            this.City = city;
            this.waiting = waiting;
            this.Dispatcher = dispatcher;
            this.turns = new Queue<Turn>();

            this.InitialResult = this.CurrentTurnResult();

            this.TurnResults = new List<TurnResult>();
            this.turns.Enqueue(new ApproachTurn(this));
        }


        public WaveResult Run()
        {
            this.TurnResults.Add(this.turns.Dequeue().Run());
            this.EnqueueCompleteRound();
            while (this.turns.Count>0 && this.WaveOver() && this.City.GameOver())
            {
                this.TurnResults.Add(this.turns.Dequeue().Run());
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