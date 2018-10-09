using System;
using System.Collections.Generic;
using System.Linq;

namespace Zarwin.Core.Engine.Tool
{
    public class ItemSelector
    {
        /// <summary>
        /// Select one item randomly from a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public T SelectItem<T>(IEnumerable<T> list)
        {
            Random rnd = new Random();
            return list.ElementAt(rnd.Next(0, list.Count()));
        }
    }
}
