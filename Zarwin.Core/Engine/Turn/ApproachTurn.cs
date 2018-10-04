using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
{
    class ApproachTurn : Turn
    {
        public ApproachTurn(Wave wave) : base(wave){}

        public override TurnResult Run()
        {
            Printer.PrintMessage("Horde in approach");
            this.wave.WaitPlayer();
            return this.wave.CurrentTurnResult();
        }
    }
}
