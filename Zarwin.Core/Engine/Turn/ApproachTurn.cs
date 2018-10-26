using Zarwin.Core.Engine.Tool;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine.Turn
{
    class ApproachTurn : Turn
    {
        public ApproachTurn(Wave wave) : base(wave){}

        /// <summary>
        /// Send a message to announce the hord
        /// </summary>
        /// <returns></returns>
        public override TurnResult Run()
        {
            UserInterface.PrintMessage("Horde in approach");
            UserInterface.ReadMessage();
            return this.wave.CurrentTurnResult;
        }
    }
}
