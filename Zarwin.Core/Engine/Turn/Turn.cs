using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine.Turn
{
    public abstract class Turn 
    {
        protected Wave wave;

        /// <summary>
        /// The base of each turn
        /// </summary>
        /// <param name="wave"></param>
        public Turn(Wave wave)
        {
            this.wave = wave;
        }

        public abstract TurnResult Run();
    }
}
