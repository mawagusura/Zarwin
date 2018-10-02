using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zarwin.Shared.Contracts.Core;

namespace Zarwin.Core.Engine
{
    class DamageDispatcher : IDamageDispatcher
    {
        private ItemSelector Selector { get; set; }

        public DamageDispatcher(ItemSelector sel)
        {
            this.Selector = sel;
        }

        void IDamageDispatcher.DispatchDamage(int damage, IEnumerable<ISoldier> soldiers)
        {
            while (damage > 0 && soldiers.Sum(soldier => soldier.HealthPoints) > 0)
            {
                ISoldier chosenSoldier;
                do
                {
                    chosenSoldier = Selector.SelectItem(soldiers);

                } while (chosenSoldier.HealthPoints==0);
                int damageDealt = Math.Min(damage, chosenSoldier.HealthPoints);

                chosenSoldier.Hurt(damageDealt);
                damage -= damageDealt;
            }
        }
    }
}
