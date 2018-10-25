using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine.Turn
{
    public class SiegeTurn : Turn
    {
        public SiegeTurn(Wave wave) : base(wave){}

        /// <summary>
        /// Run the turn of the soldier phase and zombie phase
        /// </summary>
        /// <returns></returns>
        public override TurnResult Run()
        {
            this.ZombiePhase();
            this.SoldierPhase();

            return this.wave.CurrentTurnResult();
        }

        /// <summary>
        /// Each soldier alive attack the horde
        /// </summary>
        private void SoldierPhase()
        {
            foreach (Soldier soldier in this.wave.GetCity().GetSquad().SoldiersAlive)
            {
                if (soldier.HealthPoints > 0)
                {
                    UserInterface.PrintMessage("Solider n°" + soldier.Id + " attacks");
                    this.wave.KillZombies(soldier);

                    UserInterface.ReadMessage();
                }
            }
        }

        /// <summary>
        /// The horde attack the wall, if the city got one, else they attack the soldiers.
        /// </summary>
        private void ZombiePhase()
        {
            if (this.wave.GetCity().GetWall().HealthPoints > 0)
            {
                this.wave.GetCity().GetWall().Hurt(this.wave.ZombiesAlive.Count);
                UserInterface.PrintMessage("Zombies attack wall");
            }
            else
            {
                this.wave.GetDamageDispatcher().DispatchDamage(this.wave.ZombiesAlive.Count, this.wave.GetCity().GetSquad().SoldiersAlive);
                UserInterface.PrintMessage("Zombies attack soldiers");
            }

            UserInterface.ReadMessage();
        }
    }
}
