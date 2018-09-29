using System;
using System.Collections.Generic;
using System.Text;

namespace Zarwin.Core.entity
{
    class Wave
    {
        static int nextId = 1;
        public int Id { get; private set; }

        public List<Zombie> zombies { get; private set; }

        public Wave()
        {
            Id = nextId++;
        }

    }
}
