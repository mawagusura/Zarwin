using System;
using System.Collections.Generic;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Engine.Turn;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    public class Wave
    {
        public List<Zombie> Zombies { get; set; } = new List<Zombie>();
        public City City { get; }
        private Boolean Player { get; }


        public IDamageDispatcher Dispatcher { get; }
        private TurnResult InitialResult { get; set; }
        private List<TurnResult> TurnResults { get; }

        /// <summary>
        /// Create a wave 
        /// </summary>
        /// <param name="waveParameter"></param>
        /// <param name="city"></param>
        /// <param name="dispatcher"></param>
        /// <param name="waiting"></param>
        public Wave(WaveHordeParameters waveParameter, City city, IDamageDispatcher dispatcher, Boolean waiting)
        {
            foreach(ZombieParameter z in waveParameter.ZombieTypes)
            {
                for (int i=0; i < z.Count; i++)
                {
                    Zombies.Add(new Zombie(z));
                }
            }
            this.City = city;
            this.Player = waiting;
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
            while (!this.IsOver() && !this.City.GameOver())
            {
                this.TurnResults.Add(new SiegeTurn(this).Run());
            }
        }

        /// <summary>
        /// A wave is over when all zombies died
        /// </summary>
        public Boolean IsOver() => this.Zombies.Count == 0;

        /// <summary>
        /// A soldier kill zombies based on it attack and the number of zombies still "alive"
        /// The soldier levelup foreach zombie killed
        /// </summary>
        /// <param name="soldier"></param>
        public void KillZombies(Soldier soldier)
        {
            //The soldier can kill all zombies "alive"
            if (this.Zombies.Count < soldier.AttackPoints)
            {
                soldier.LevelUp(this.Zombies.Count);
                Zombies = new List<Zombie>();
            }
            
            //Soldier kills all zombies he can 
            else
            {
                this.Zombies.RemoveRange(0, soldier.AttackPoints);
                soldier.LevelUp(soldier.AttackPoints);
            }
        }

        /// <summary>
        /// If the wave is played by a player, each phase the player have to valide
        /// </summary>
        public void WaitPlayer()
        {
            UserInterface.ReadMessage(this.Player);
        }

        /// <summary>
        /// Print a message to the user
        /// </summary>
        /// <param name="message"></param>
        public void PrintMessage(String message)
        {
            UserInterface.PrintMessage(message, this.Player);
        }

        //States 

        /// <summary>
        /// Create an HordeState of the current situation
        /// </summary>
        /// <returns></returns>
        private HordeState HordeState() => new HordeState(this.Zombies.Count);

        /// <summary>
        /// Create a TurnResult of the current situation
        /// </summary>
        /// <returns></returns>
        public TurnResult CurrentTurnResult()
           => new TurnResult(this.City.SoldierState.ToArray(), this.HordeState(), this.City.WallHealthPoints, this.City.Money);

        /// <summary>
        /// Create a WaveResult of the current situation
        /// </summary>
        /// <returns></returns>
        public WaveResult WaveResult()=> new WaveResult(this.InitialResult, this.TurnResults.ToArray());
    }
}