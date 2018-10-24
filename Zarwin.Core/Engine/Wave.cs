using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Zombie> Zombies { get;} = new List<Zombie>();
        public City City { get; }
        private Boolean Player { get; }


        public IDamageDispatcher Dispatcher { get; }
        private TurnResult InitialResult { get; set; }
        private List<TurnResult> TurnResults { get; }

        public List<Order> Orders { get; } = new List<Order>();

        public List<Zombie> ZombiesAlive => Zombies.Where(z => z.HealthPoints > 0).ToList();

        /// <summary>
        /// Create a wave 
        /// </summary>
        /// <param name="waveParameter"></param>
        /// <param name="city"></param>
        /// <param name="dispatcher"></param>
        /// <param name="waiting"></param>
        public Wave(WaveHordeParameters waveParameter, City city, IDamageDispatcher dispatcher, Boolean waiting, List<Order> orders)
        {
            foreach(ZombieParameter z in waveParameter.ZombieTypes)
            {
                for (int i=0; i < z.Count; i++)
                {
                    Zombies.Add(new Zombie(z));
                }
            }

            // tri liste zombies
            Zombies.Sort();

            this.City = city;
            this.Player = waiting;
            this.Dispatcher = dispatcher;
            this.Orders.AddRange(orders);
            
            this.InitialResult = this.CurrentTurnResult();

            this.TurnResults = new List<TurnResult>();

            ExecuteOrder();
            this.City.ExecuteActions();
        }



        /// <summary>
        /// Run a wave, start with the ApproachTurn and run SiegeTurn while there is still zombies or soldier
        /// </summary>
        public void Run()
        {
            this.TurnResults.Add(new ApproachTurn(this).Run());
            while (!this.IsOver() && !this.City.GameOver())
            {
                ExecuteOrder();
                this.TurnResults.Add(new SiegeTurn(this).Run());
            }
        }

        /// <summary>
        /// A wave is over when all zombies died
        /// </summary>
        public Boolean IsOver() => ZombiesAlive.Count==0;

        /// <summary>
        /// A soldier kill zombies based on it attack and the number of zombies still "alive"
        /// The soldier levelup foreach zombie killed
        /// </summary>
        /// <param name="soldier"></param>
        public void KillZombies(Soldier soldier)
        {
            Zombies.Sort();
            int attack = soldier.AttackPoints;

            while (attack > 0 && ZombiesAlive.Count>0)
            {
                Zombie temp = ZombiesAlive[0];
                int def = temp.HealthPoints;
                temp.Hurt(attack, TurnResults.Count);
                attack -= def;
                if (temp.HealthPoints == 0)
                {
                    soldier.LevelUp();
                    this.City.IncreaseMoney(1);
                }
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
        private HordeState HordeState()
            => new HordeState(ZombiesAlive.Count);
        
        

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

        private void ExecuteOrder()
        {
            foreach(Order o in this.Orders.Where(order => order.TurnIndex == this.TurnResults.Count))
            {
                this.City.ExecuteOrder(o.Type,o.TargetSoldier);
            }
        }
    }
}