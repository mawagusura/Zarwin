using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zarwin.Core.Engine
{
    class ItemSelector
    {
        public T SelectItem<T>(IEnumerable<T> list)
        {
            Random rnd = new Random();
            return list.ElementAt(rnd.Next(0, list.Count()-1));
        }
    }
}
