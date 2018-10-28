using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity.Cities;
using Zarwin.Core.Entity.Squads.Weapons;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity.Squads
{
    public class Squad
    {
        private int soldierId = 1;
        private List<Soldier> Soldiers { get; } = new List<Soldier>();

        public City City { get; }

        public SoldierState[] SoldierStates
            => SoldiersAlive.Select(s => new SoldierState(s.Id, s.Level, s.HealthPoints)).ToArray();

        public List<Soldier> SoldiersWithoutWeapon => SoldiersAlive.Where(soldier => soldier.Weapon is Hand).ToList();

        public List<Soldier> SoldiersAlive => Soldiers.Where(soldier => soldier.HealthPoints > 0).ToList();

        public Soldier SoliderById(int id) => Soldiers.Where(soldier => soldier.Id == id).ToArray()[0];

        public Boolean IsAlive
            => this.SoldiersAlive.Count > 0;
        

        public Squad() : this(new SoldierParameters[0]) { }
        public Squad(SoldierParameters[] soldierParameters) : this(soldierParameters, new City()) { }

        public Squad(SoldierParameters[] soldierParameters, City city)
        {
            foreach (SoldierParameters sp in soldierParameters)
            {
                this.Soldiers.Add(new Soldier(sp, this));
                this.soldierId++;
            }
            this.City = city;
        }

        public void RecruitSoldier()
        {
            this.Soldiers.Add(new Soldier(soldierId++,this));
        }
    }
}
