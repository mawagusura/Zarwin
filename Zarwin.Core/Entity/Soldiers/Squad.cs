using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity.Soldiers.Weapons;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity.Soldiers
{
    public class Squad
    {

        private List<Soldier> soldiers = new List<Soldier>();

        public List<SoldierState> SoldierState
            => SoldiersAlive.Select(s => new SoldierState(s.Id, s.Level, s.HealthPoints)).ToList();

        public List<Soldier> SoldiersWithoutWeapon => SoldiersAlive.Where(soldier => soldier.Weapon is Hand).ToList();

        public List<Soldier> SoldiersAlive => soldiers.Where(soldier => soldier.HealthPoints > 0).ToList();

        public Soldier SoliderById(int id) => soldiers.Where(soldier => soldier.Id == id).ToArray()[0];

        public Squad(List<SoldierParameters> soldierParameters)
        {

            // initilaize Soldiers with parameters
            soldierParameters.ForEach(sp => soldiers.Add(new Soldier(sp)));
        }

        public void RecruitSoldier()
        {
            this.soldiers.Add(new Soldier());
        }

        public Boolean IsAlive
            => (this.soldiers.Sum(soldier => soldier.HealthPoints) > 0) && (this.soldiers.Count > 0);
    }
}
