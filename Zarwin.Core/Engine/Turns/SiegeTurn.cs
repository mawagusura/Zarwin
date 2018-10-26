using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity.Soldiers;
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

            return this.wave.CurrentTurnResult;
        }

        /// <summary>
        /// Each soldier alive attack the horde
        /// </summary>
        private void RunSoldierPhase()
        {
            foreach (Soldier soldier in this.wave.City.Squad.SoldiersAlive)
            {
                if (soldier.HealthPoints > 0)
                {
                    UserInterface.PrintMessage("Solider n°" + soldier.Id + " attacks");
                    this.wave.City.IncreaseMoney(this.wave.Horde.AttackZombies(soldier,this.wave.TurnCount));

                    UserInterface.ReadMessage();
                }
            }
        }

        /// <summary>
        /// The horde attack the wall, if the city got one, else they attack the soldiers.
        /// </summary>
        private void RunZombiePhase()
        {
            if (this.wave.City.Wall.HealthPoints > 0)
            {
                this.wave.City.Wall.Hurt(this.wave.Horde.ZombiesAlive.Count);
                UserInterface.PrintMessage("Zombies attack wall");
            }
            else
            {
                this.wave.Dispatcher.DispatchDamage(this.wave.Horde.ZombiesAlive.Count, this.wave.City.Squad.SoldiersAlive);
                UserInterface.PrintMessage("Zombies attack soldiers");
            }

            UserInterface.ReadMessage();
        }
    }
}
