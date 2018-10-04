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

        /// <summary>
        /// Create a wave 
        /// </summary>
        /// <param name="hordeParameter"></param>
        /// <param name="city"></param>
        /// <param name="dispatcher"></param>
        /// <param name="waiting"></param>
        public Wave(HordeParameters hordeParameter, City city, IDamageDispatcher dispatcher, Boolean waiting)
        {
            this.Zombies = hordeParameter.Size;
            this.City = city;
            this.waiting = waiting;
            this.Dispatcher = dispatcher;

            this.InitialResult = this.CurrentTurnResult();

            this.TurnResults = new List<TurnResult>();
        }

        /// <summary>
        /// Run a wave, start with the ApproachTurn and run SiegeTurn while there is still zombies or soldier
        /// </summary>
        public void Run()
        {
            this.TurnResults.Add(new ApproachTurn(this).Run());
            while (!this.WaveOver() && !this.City.GameOver())
            {
                this.TurnResults.Add(new SiegeTurn(this).Run());
            }
        }

        /// <summary>
        /// A wave is over when all zombies died
        /// </summary>
        public Boolean WaveOver() => this.Zombies == 0;

        /// <summary>
        /// A soldier kill zombies based on it attack and the number of zombies still "alive"
        /// The soldier levelup foreach zombie killed
        /// </summary>
        /// <param name="soldier"></param>
        public void KillZombies(Soldier soldier)
        {
            //The soldier can kill all zombies "alive"
            if (this.Zombies < soldier.AttackPoints)
            {
                soldier.LevelUp(this.Zombies);
                this.Zombies = 0;
            }
            
            //Soldier kills all zombies he can 
            else
            {
                this.Zombies -= soldier.AttackPoints;
                soldier.LevelUp(soldier.AttackPoints);
            }
        }

        /// <summary>
        /// If the wave is played by a player, each phase the player have to valide
        /// </summary>
        public void WaitPlayer()
        {
            if (this.waiting)
            {
                Console.ReadLine();
            }
        }

        //States 

        /// <summary>
        /// Create an HordeState of the current situation
        /// </summary>
        /// <returns></returns>
        private HordeState HordeState() => new HordeState(this.Zombies);

        /// <summary>
        /// Create a TurnResult of the current situation
        /// </summary>
        /// <returns></returns>
        public TurnResult CurrentTurnResult()
           => new TurnResult(this.City.SoldierState.ToArray(), this.HordeState(), this.City.WallHealthPoints);

        /// <summary>
        /// Create a WaveResult of the current situation
        /// </summary>
        /// <returns></returns>
        public WaveResult WaveResult()=> new WaveResult(this.InitialResult, this.TurnResults.ToArray());
    }
}