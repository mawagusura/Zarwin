using System;

namespace Zarwin.Core.Engine.Tool
{

    public class UserInterface
    {
        private event ShowMessage MessageHandler;
        private delegate void ShowMessage(String s);

        public UserInterface(Boolean b)
        {
            this.MessageHandler = null;
            if (b)
            {
                this.MessageHandler = PrintMessage;
            }
        }

        private void PrintMessage(String message)
        {
                Console.WriteLine(message); 
        }

        public String ReadMessage()
        {
            return (this.MessageHandler != null) ? Console.ReadLine() : "";
        }

        public void InvokeSoliderHit(int soldierId, int healthpoints)
        {
                this.MessageHandler?.Invoke($"Soldat #{soldierId} a perdu {healthpoints} PV.");
        }

        public void InvokeSoliderDown(int soldierId)
        {
            this.MessageHandler?.Invoke($"Soldat #{soldierId} est tombé.");
        }

        public void InvokeSoliderKill(int soldierId, int zombies)
        {
            this.MessageHandler?.Invoke($"Soldat #{soldierId} a tué {zombies} zombies.");
        }

        public void InvokeEndTurn(int zombies)
        {
            this.MessageHandler?.Invoke($"Fin du tour. Reste {zombies} zombies.");
        }

        public void InvokeEndWave()
        {
            this.MessageHandler?.Invoke("Fin de vague.");
        }

        public void InvokeApproach()
        {
            this.MessageHandler?.Invoke("Horde en approche.");
        }
    }
}
