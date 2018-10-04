using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Shared.Contracts.Core;

namespace Zarwin.Core.Engine
{
    class DamageDispatcher : IDamageDispatcher
    {
        private ItemSelector Selector { get; set; }

        /// <summary>
        /// Constructor of DamageDisptacher
        /// The selection used a Selector
        /// </summary>
        /// <param name="sel"></param>
        public DamageDispatcher(ItemSelector sel)
        {
            this.Selector = sel;
        }

        /// <summary>
        /// Dispatch damage between soldiers
        /// The soldier select take all damage he can
        /// (max damage to split or max heal point)
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="soldiers"></param>
        void IDamageDispatcher.DispatchDamage(int damage, IEnumerable<ISoldier> soldiers)
        {
            //Dispatch damage while there is damage to splite and there is still soldier alive
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
