using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Engine.Turns;
using Zarwin.Core.Entity.Cities;
using Zarwin.Core.Entity.Zombies;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    public class Wave
    {
        public City City { get; }
        public IDamageDispatcher Dispatcher { get; }
        public Horde Horde { get; }

        private readonly TurnResult initialResult;
        private readonly List<TurnResult> turnResults = new List<TurnResult>();
        private readonly List<Order> orders = new List<Order>();

        // States of the wave 
        public TurnResult CurrentTurnResult
           => new TurnResult(this.City.Squad.SoldierState.ToArray(), this.Horde.HordeState, this.City.Wall.HealthPoints, this.City.Money);

        public WaveResult WaveResult
            => new WaveResult(this.initialResult, this.turnResults.ToArray());

        public int TurnCount
            => this.turnResults.Count;

        /// <param name="waveParameter"></param>
        /// <param name="city"></param>
        /// <param name="dispatcher"></param>
        /// <param name="waiting"></param>
        public Wave(WaveHordeParameters waveParameter, City city, IDamageDispatcher dispatcher, List<Order> orders)
        {
            this.Horde = new Horde(waveParameter.ZombieTypes);
            this.City = city;
            this.Dispatcher = dispatcher;
            this.orders.AddRange(orders);
            
            this.initialResult = this.CurrentTurnResult;

            ExecuteOrder();
            this.City.ExecuteActions();
        }

        /// <summary>
        /// Run a wave, start with the ApproachTurn and run SiegeTurn while there is still zombies or soldier
        /// </summary>
        public void Run()
        {
            this.turnResults.Add(new ApproachTurn(this).Run());
            while (!this.Horde.IsAlive() && this.City.Squad.IsAlive)
            {
                ExecuteOrder();
                this.turnResults.Add(new SiegeTurn(this).Run());
            }
        }

        private void ExecuteOrder()
        {
            foreach(Order o in this.orders.Where(order => order.TurnIndex == this.turnResults.Count))
            {
                this.City.ExecuteOrder(o.Type,o.TargetSoldier);
            }
        }
        
    }
}