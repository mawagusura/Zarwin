using Zarwin.Core.Entity.Squads;
using Zarwin.Core.Entity.Waves;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine.Turns { 
    public class SiegeTurn : Turn
    {
        public SiegeTurn(Wave wave) : base(wave){}

        /// <summary>
        /// Run the turn of the soldier phase and zombie phase
        /// </summary>
        /// <returns></returns>
        public override TurnResult Run()
        {
            this.RunZombiePhase();
            this.RunSoldierPhase();

            return this.Wave.CurrentTurnResult;
        }

        /// <summary>
        /// Each soldier alive attack the horde
        /// </summary>
        private void RunSoldierPhase()
        {
            foreach (Soldier soldier in this.Wave.City.Squad.SoldiersAlive)
            {
                this.Wave.City.IncreaseMoney(this.Wave.Horde.AttackZombies(soldier,this.Wave.TurnResults.Count));

                this.Wave.City.UserInterface.ReadMessage();
            }
        }

        /// <summary>
        /// The horde attack the wall, if the city got one, else they attack the soldiers.
        /// </summary>
        private void RunZombiePhase()
        {
            if (this.Wave.City.Wall.HealthPoints > 0)
            {
                this.Wave.City.Wall.Hurt(this.Wave.Horde.ZombiesAlive.Count);
            }
            else
            {
                this.Wave.DamageDispatcher.DispatchDamage(
                    this.Wave.Horde.ZombiesAlive.Count, this.Wave.City.Squad.SoldiersAlive);
            }

            this.Wave.City.UserInterface.ReadMessage();
        }
    }
}
