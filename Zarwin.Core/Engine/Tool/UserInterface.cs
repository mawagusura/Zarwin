using System;

namespace Zarwin.Core.Engine.Tool
{
    
    public class UserInterface
    {
        private static Boolean userPlaying;

        public static void SetUserPlaying(Boolean b)
        {
            UserInterface.userPlaying = b;
        }

        /// <summary>
        /// Print a line into the console
        /// </summary>
        /// <param name="message"></param>
        public static void PrintMessage(String message) 
        {
            if (UserInterface.userPlaying)
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Read a line from the console
        /// </summary>
        /// <returns></returns>
        public static String ReadMessage()
        {
            return (UserInterface.userPlaying) ?Console.ReadLine():"";
        }
    }
}
