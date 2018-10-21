using System;
using System.Collections.Generic;
using System.Text;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity
{
    public class Zombie
    {
        public ZombieTrait Trait { get; }

        public ZombieType Type { get; }

        public Zombie(ZombieParameter zombieParameter)
        {
            Trait = zombieParameter.Trait;
            Type = zombieParameter.Type;
        }
    }
}
