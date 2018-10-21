﻿using System;
namespace Zarwin.Core.Engine.Tool
{
    
    public class UserInterface
    {
        /// <summary>
        /// Print a line into the console
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
        /// Read a line from the console
        /// </summary>
        /// <returns></returns>
        public static String ReadMessage(bool userPlay)
        {
            return (userPlay)?Console.ReadLine():"";
        }
    }
}