using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine.Turn
{
    class SiegeTurn : Turn
    {
        public SiegeTurn(Wave wave) : base(wave){}

        /// <summary>
        /// Run the turn of the soldier phase and zombie phase
        /// </summary>
        /// <returns></returns>
        public override TurnResult Run()
        {
            this.SoldierPhase();
            if (this.wave.WaveOver())
            {
                return this.wave.CurrentTurnResult();
            }
            this.ZombiePhase();

            return this.wave.CurrentTurnResult();
        }

        /// <summary>
        /// Each soldier alive attack the horde
        /// </summary>
        private void SoldierPhase()
        {
            foreach (Soldier soldier in this.wave.City.Soldiers)
            {
                if (soldier.HealthPoints > 0)
                {
                    this.wave.PrintMessage("Solider n°" + soldier.Id + " attacks");
                    this.wave.KillZombies(soldier);

                    this.wave.WaitPlayer();
                }
            }
        }

        /// <summary>
        /// The horde attack the wall, if the city got one, else they attack the soldiers.
        /// </summary>
        private void ZombiePhase()
        {
            if (this.wave.City.WallHealthPoints > 0)
            {
                this.wave.City.HurtWall(this.wave.Zombies);
                this.wave.PrintMessage("Zombies attack wall");
            }
            else
            {
                this.wave.Dispatcher.DispatchDamage(this.wave.Zombies, this.wave.City.Soldiers);
                this.wave.PrintMessage("Zombies attack soldiers");
            }

            this.wave.WaitPlayer();
        }
    }
}
