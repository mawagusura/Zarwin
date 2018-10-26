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
        private readonly City city;
        private readonly IDamageDispatcher dispatcher;
        private readonly TurnResult initialResult;
        private readonly List<TurnResult> turnResults = new List<TurnResult>();
        private readonly List<Order> orders = new List<Order>();
        private readonly Horde horde;
        
        /// <param name="waveParameter"></param>
        /// <param name="city"></param>
        /// <param name="dispatcher"></param>
        /// <param name="waiting"></param>
        public Wave(WaveHordeParameters waveParameter, City city, IDamageDispatcher dispatcher, List<Order> orders)
        {
            this.horde = new Horde(waveParameter.ZombieTypes);
            this.city = city;
            this.dispatcher = dispatcher;
            this.orders.AddRange(orders);
            
            this.initialResult = this.CurrentTurnResult();

            ExecuteOrder();
            this.city.ExecuteActions();
        }

        public int GetTurnNumber()
        {
            return this.turnResults.Count;
        }



        /// <summary>
        /// Run a wave, start with the ApproachTurn and run SiegeTurn while there is still zombies or soldier
        /// </summary>
        public void Run()
        {
            this.turnResults.Add(new ApproachTurn(this).Run());
            while (!this.horde.IsAlive() && !this.city.GetSquad().IsAlive())
            {
                ExecuteOrder();
                this.turnResults.Add(new SiegeTurn(this).Run());
            }
        }
        
       

        //States
        

        /// <summary>
        /// Create a TurnResult of the current situation
        /// </summary>
        /// <returns></returns>
        public TurnResult CurrentTurnResult()
           => new TurnResult(this.city.GetSquad().SoldierState.ToArray(), this.horde.HordeState(), this.city.GetWall().HealthPoints, this.city.Money);

        
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
        public Horde GetHorde()
        {
            return this.horde;
        }
        
        public IDamageDispatcher GetDamageDispatcher()
        {
            return this.dispatcher;
        }
    }
}