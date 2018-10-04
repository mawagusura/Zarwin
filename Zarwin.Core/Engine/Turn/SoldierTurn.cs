using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    class SoldierTurn : Turn
    {
        private Soldier soldier;

        public SoldierTurn(Wave wave,Soldier soldier) : base(wave)
        {
            this.soldier = soldier;
        }

        public override TurnResult Run()
        {
            Printer.PrintMessage("Solider " + soldier.Id + "kills " + soldier.AttackPoints + "zombies");
            this.wave.KillZombie(soldier.AttackPoints);

            this.wave.WaitPlayer();

            return this.wave.CurrentTurnResult();
        }
    }
}
