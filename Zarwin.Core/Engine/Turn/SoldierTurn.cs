using System;
using System.Collections.Generic;
using System.Text;
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
            if (soldier.AttackPoints >= this.wave.Zombies.Count)
            {
                Printer.PrintMessage("Solider " + soldier.Id + " kills " + Math.Min(this.wave.Zombies.Count, soldier.AttackPoints) + " zombie(s)");
                this.wave.Zombies.Clear();
            }
            else
            {
                Printer.PrintMessage("Solider " + soldier.Id + "kills " + soldier.AttackPoints + "zombies");
                for (int i = 0; i < soldier.AttackPoints; i++)
                {
                    this.wave.Zombies.RemoveAt(0);
                }
            }

            this.wave.WaitPlayer();

            return (this.wave.Zombies.Count > 0) ? this.wave.CurrentTurnResult() : null;
        }
    }
}
