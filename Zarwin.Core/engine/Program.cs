using System;
using System.Collections.Generic;
using System.Text;
using Zarwin.Core.entity;

namespace Zarwin.Core.engine
{
    class Program
    {

        public static int Main()
        {

            Soldier s1 = new Soldier();
            Console.WriteLine("Soldier " + s1.Id + "created. Health : "+s1.HealthPoints);
            s1.Hurt(1);
            Console.WriteLine("New Health : "+s1.HealthPoints);
            Console.ReadLine();

            return 0;
        }
    }
}
