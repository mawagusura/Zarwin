using System;
using System.Diagnostics.CodeAnalysis;

namespace Zarwin.Core.Engine.Tool
{
    [ExcludeFromCodeCoverage]
    class UserInterface
    {
        /// <summary>
        /// Print a message into the console
        /// </summary>
        /// <param name="message"></param>
        public static void PrintMessage(String message, bool userPlay) 
        {
            if (userPlay)
            {
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// Read a message from the console
        /// </summary>
        /// <returns></returns>
        public static String ReadMessage(bool userPlay)
        {
            return (userPlay)?Console.ReadLine():"";
        }
    }
}
