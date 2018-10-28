using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Engine;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity.Cities;
using Zarwin.Shared.Contracts.Core;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity.Waves
{
    public class Wave
    {
        public Horde Horde { get; }
        public City City { get; }
        public IDamageDispatcher DamageDispatcher { get; }
        public WaveEngine WaveEngine { get; }

        public TurnResult InitialResult { get; }
        public List<TurnResult> TurnResults { get; } = new List<TurnResult>();
        public List<Order> Orders { get; } = new List<Order>();

        public TurnResult CurrentTurnResult
           => new TurnResult(this.City.Squad.SoldierStates, this.Horde.HordeState, this.City.Wall.HealthPoints, this.City.Money);

        public WaveResult WaveResult
            => new WaveResult(this.InitialResult, this.TurnResults.ToArray());

        public Wave() : this(new ZombieParameter[0], new City(), new List<Order>(), new DamageDispatcher(new ItemSelector())) { }

        public Wave(ZombieParameter[] zombieParameters, City city, List<Order> orders,IDamageDispatcher damageDispatcher)
        {
            this.Horde = new Horde(zombieParameters, city);
            this.City = city;
            this.Orders.AddRange(orders);
            this.InitialResult = this.CurrentTurnResult;
            this.DamageDispatcher = damageDispatcher;
            this.City.OrderHandler.BuyOrders(this.Orders.Where(order => order.TurnIndex == this.TurnResults.Count));
            this.WaveEngine = new WaveEngine(this);
        }

        /// <summary>
        /// Run a wave, start with the ApproachTurn and run SiegeTurn while there is still zombies or soldier
        /// </summary>
        public void Run()
        {
            this.WaveEngine.Run();
        }
    }
}