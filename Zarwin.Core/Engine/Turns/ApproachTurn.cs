using Zarwin.Core.Entity.Waves;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine.Turns
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
            this.Wave.City.UserInterface.InvokeApproach();
            this.Wave.City.UserInterface.ReadMessage();
            return this.Wave.CurrentTurnResult;
        }
    }
}
