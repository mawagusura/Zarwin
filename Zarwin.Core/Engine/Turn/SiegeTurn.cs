using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    class SiegeTurn : Turn
    {
        public SiegeTurn(Wave wave) : base(wave){}

        public override TurnResult Run()
        {
            this.SoldierPhase();
            this.ZombiePhase();

            return this.wave.CurrentTurnResult();
        }

        private void SoldierPhase()
        {
            foreach (Soldier soldier in this.wave.City.Soldiers)
            {
                if (soldier.HealthPoints > 0)
                {
                    Printer.PrintMessage("Solider " + soldier.Id + "kills " + soldier.AttackPoints + "zombies");
                    this.wave.KillZombie(soldier);

                    this.wave.WaitPlayer();
                }
            }
        }

        private void ZombiePhase()
        {
            if (this.wave.City.WallHealthPoints > 0)
            {
                this.wave.City.HurtWall(this.wave.Zombies);
                Printer.PrintMessage("Zombies attack wall");
            }
            else
            {
                this.wave.Dispatcher.DispatchDamage(this.wave.Zombies, this.wave.City.Soldiers);
                Printer.PrintMessage("Zombies attack soldiers");
            }

            this.wave.WaitPlayer();
        }
    }
}
