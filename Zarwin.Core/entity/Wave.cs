using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zarwin.Core.Entity
{
    public class Wave
    {
        static int nextId = 1;
        public int Id { get; private set; }

        private const int nbZombie = 10;

        public List<Zombie> zombies { get; private set; }

        public Wave()
        {
            Id = nextId++;
            zombies = Enumerable.Repeat(new Zombie(), nbZombie).ToList();
        }

    }
}
