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

        public Wave(HordeParameters hordeParameter, City city, IDamageDispatcher dispatcher, Boolean waiting)
        {
            this.Zombies = hordeParameter.Size;
            this.City = city;
            this.waiting = waiting;
            this.Dispatcher = dispatcher;

            this.InitialResult = this.CurrentTurnResult();

            this.TurnResults = new List<TurnResult>();
        }


        public WaveResult Run()
        {
            this.TurnResults.Add(new ApproachTurn(this).Run());
            while (!this.WaveOver())
            {
                this.TurnResults.Add(new SiegeTurn(this).Run());
            }
            return new WaveResult(this.InitialResult,this.TurnResults.ToArray());
        }

        public Boolean WaveOver() => this.Zombies == 0;

        public void KillZombie(Soldier soldier)
        {
            if (this.Zombies < soldier.AttackPoints)
            {
                soldier.LevelUp(this.Zombies);
                this.Zombies = 0;
            }
            else
            {
                this.Zombies -= soldier.AttackPoints;
                soldier.LevelUp(soldier.AttackPoints);
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