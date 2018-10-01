using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zarwin.Shared.Contracts.Core;

namespace Zarwin.Core.Engine
{
    class DamageDispatcher : IDamageDispatcher
    {
        void IDamageDispatcher.DispatchDamage(int damage, IEnumerable<ISoldier> soldiers)
        {
            while (damage > 0 && soldiers.Sum(soldier => soldier.HealthPoints) > 0)
            {
                Random rnd = new Random();
                ISoldier chosenSoldier;
                do
                {
                    chosenSoldier = soldiers.ElementAt(rnd.Next(0, soldiers.Count()));

                } while (chosenSoldier.HealthPoints>0);
                int damageDealt = Math.Min(damage, chosenSoldier.HealthPoints);

                chosenSoldier.Hurt(damageDealt);
                damage -= damageDealt;
            }
        }
    }
}
