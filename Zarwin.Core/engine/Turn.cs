using System;

namespace Zarwin.Core.Engine
{
    public abstract class Turn
    {
    protected Boolean Console {get;}

    public Turn(Boolean console)
        {
            this.Console = console;
        }

        abstract public void Run();
    }
}
