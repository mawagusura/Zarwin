using System.Linq;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    class ZombieTurn : Turn
    {
        public ZombieTurn(Wave wave): base(wave){}

        public override TurnResult Run()
        {
            if (this.wave.City.WallHealthPoints > 0)
            {
                this.wave.City.HurtWall(this.wave.Zombies.Count);
                Printer.PrintMessage("Zombies attack wall");
            }
            else
            {
                this.wave.Dispatcher.DispatchDamage(this.wave.Zombies.Count, this.wave.City.Soldiers);
                Printer.PrintMessage("Zombies attack soldiers");
            }

            this.wave.WaitPlayer();

            this.wave.EnqueueCompleteRound();
            return (this.wave.City.Soldiers.Sum(soldier => soldier.HealthPoints) > 0)? this.wave.CurrentTurnResult(): null;
        }
    }
}
