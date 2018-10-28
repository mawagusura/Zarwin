using System.Linq;
using Zarwin.Core.Engine.Turns;
using Zarwin.Core.Entity.Waves;

namespace Zarwin.Core.Engine
{
    public class WaveEngine
    {
        private Wave Wave {get;}
        public WaveEngine(Wave wave)
        {
            this.Wave = wave;
        }
        public void Run()
        {
            this.Wave.TurnResults.Add(new ApproachTurn(this.Wave).Run());
            while (this.Wave.Horde.IsAlive && this.Wave.City.Squad.IsAlive)
            {
                this.Wave.City.OrderHandler.BuyOrders(this.Wave.Orders.Where(order => order.TurnIndex == this.Wave.TurnResults.Count));
                this.Wave.TurnResults.Add(new SiegeTurn(this.Wave).Run());
                this.Wave.City.UserInterface.InvokeEndTurn(this.Wave.Horde.ZombiesAlive.Count);
            }
        }
    }
}