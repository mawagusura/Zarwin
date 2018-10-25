using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Engine.Turn;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    public class Wave
    {
        private List<Zombie> zombies = new List<Zombie>();
        private readonly City city;
        private readonly IDamageDispatcher dispatcher;
        private readonly TurnResult initialResult;
        private List<TurnResult> turnResults = new List<TurnResult>();
        private readonly List<Order> orders = new List<Order>();

        public List<Zombie> ZombiesAlive => zombies.Where(z => z.HealthPoints > 0).ToList();

        
        /// <param name="waveParameter"></param>
        /// <param name="city"></param>
        /// <param name="dispatcher"></param>
        /// <param name="waiting"></param>
        public Wave(WaveHordeParameters waveParameter, City city, IDamageDispatcher dispatcher, List<Order> orders)
        {
            foreach(ZombieParameter z in waveParameter.ZombieTypes)
            {
                for (int i=0; i < z.Count; i++)
                {
                    zombies.Add(new Zombie(z));
                }
            }
            // Sort zombies
            zombies.Sort();
            this.city = city;
            this.dispatcher = dispatcher;
            this.orders.AddRange(orders);
            
            this.initialResult = this.CurrentTurnResult();

            ExecuteOrder();
            this.city.ExecuteActions();
        }



        /// <summary>
        /// Run a wave, start with the ApproachTurn and run SiegeTurn while there is still zombies or soldier
        /// </summary>
        public void Run()
        {
            this.turnResults.Add(new ApproachTurn(this).Run());
            while (!this.IsOver && !this.city.GetSquad().IsAlive())
            {
                ExecuteOrder();
                this.turnResults.Add(new SiegeTurn(this).Run());
            }
        }

        /// <summary>
        /// A wave is over when all zombies died
        /// </summary>
        public Boolean IsOver => ZombiesAlive.Count==0;

        /// <summary>
        /// A soldier kill zombies based on it attack and the number of zombies still "alive"
        /// The soldier levelup foreach zombie killed
        /// </summary>
        /// <param name="soldier"></param>
        public void KillZombies(Soldier soldier)
        {
            zombies.Sort();
            int attack = soldier.AttackPoints;

            while (attack > 0 && !IsOver)
            {
                Zombie temp = ZombiesAlive[0];
                int def = temp.HealthPoints;
                temp.Hurt(attack, turnResults.Count);
                attack -= def;
                if (temp.HealthPoints == 0)
                {
                    soldier.LevelUp();
                    this.city.IncreaseMoney(1);
                }
            }
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
           => new TurnResult(this.city.GetSquad().SoldierState.ToArray(), this.HordeState(), this.city.GetWall().HealthPoints, this.city.Money);

        
        public WaveResult WaveResult()=> new WaveResult(this.initialResult, this.turnResults.ToArray());

        
        private void ExecuteOrder()
        {
            foreach(Order o in this.orders.Where(order => order.TurnIndex == this.turnResults.Count))
            {
                this.city.ExecuteOrder(o.Type,o.TargetSoldier);
            }
        }

        public City GetCity()
        {
            return this.city;
        }
        
        public IDamageDispatcher GetDamageDispatcher()
        {
            return this.dispatcher;
        }
    }
}