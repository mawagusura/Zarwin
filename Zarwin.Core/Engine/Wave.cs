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
        private readonly TurnResult initialResult;
        private readonly List<TurnResult> turnResults = new List<TurnResult>();
        private readonly List<Order> orders = new List<Order>();

        public City City { get; }
        public IDamageDispatcher Dispatcher { get; }
        public Horde Horde { get; }

        // States of the wave
        public TurnResult CurrentTurnResult
           => new TurnResult(this.City.Squad.SoldierState.ToArray(), this.Horde.HordeState, this.City.Wall.HealthPoints, this.City.Money);

        public WaveResult WaveResult
            => new WaveResult(this.initialResult, this.turnResults.ToArray());

        // Constructor
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
            while (!this.Horde.IsAlive() && !this.City.Squad.IsAlive())
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

        public int GetTurnCount()
        {
            return this.turnResults.Count;
        }

    }
}