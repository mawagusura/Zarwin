using System;
using System.Collections.Generic;
using System.Text;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Engine
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
