using Zarwin.Core.Entity.Waves;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine.Turns
{
    public abstract class Turn 
    {
        protected Wave Wave { get; }

        /// <summary>
        /// The base of each turn
        /// </summary>
        /// <param name="wave"></param>
        public Turn(Wave wave)
        {
            this.Wave = wave;
        }

        public abstract TurnResult Run();
    }
}
