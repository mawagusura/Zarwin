using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity.Weapon;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity
{
    public class City
    {
        private const int PriceUpgrade = 10;

        public int WallHealthPoints { get; set; } = 10;

        public List<Soldier> Soldiers { get; } = new List<Soldier>();

        public int Money { get; set;  } = 0;

        public Queue<Action> Actions { get; } = new Queue<Action>();

        public List<SoldierState> SoldierState
        {
            get
            {
                List<SoldierState> states = new List<SoldierState>();
                Soldiers.Where(sold => sold.HealthPoints>0).ToList()
                        .ForEach(sold => states.Add(new SoldierState(sold.Id, sold.Level, sold.HealthPoints)));
                return states;
            }
        }
        
        public List<Soldier> SoldierWithoutWeapon => Soldiers.Where( soldier => soldier.Weapon.GetType()== typeof(Hand)).ToList();

        public Soldier SoliderById(int id) => Soldiers.Where(soldier => soldier.Id == id).ToArray()[0];

        /// <summary>
        ///Empty constructor for unit tests
        /// </summary>
        public City()
        {
        }

        /// <summary>
        /// Constructor of the City class
        /// </summary>
        /// <param name="soldierParameters"></param>
        public City(CityParameters cityParameter ,List<SoldierParameters> soldierParameters)
        {
            WallHealthPoints = cityParameter.WallHealthPoints;
            this.Money = cityParameter.InitialMoney;
            // initilaize Soldiers with parameters
            soldierParameters.ForEach(sp => Soldiers.Add(new Soldier(sp,this)));
        }

        /// <summary>
        /// Hit the wall with damage given in param
        /// </summary>
        /// <param name="amount"></param>
        public void HurtWall(int amount)
        {
            WallHealthPoints= amount > WallHealthPoints ?  0 : WallHealthPoints - amount;
        }

        /// <summary>
        /// Test is the game is over, there is no soldier or there are all dead
        /// </summary>
        /// <returns></returns>
        public Boolean GameOver()
        {
            return (this.Soldiers.Sum(soldier => soldier.HealthPoints) == 0) || (this.Soldiers.Count==0);
        }


        public void IncreaseMoney(int money)
        {
            this.Money += money;
        }
        public void ExecuteActions()
        {
            while (this.Actions.Count > 0)
            {
                this.Actions.Dequeue()();
            }
        }

        public void ExecuteOrder(OrderType orderType,int? targetSoldier)
        {
            if (Money >= PriceUpgrade)
            {
                Money -= PriceUpgrade;
            }
            else
            {
                return;
            }
                switch (orderType)
            {
                case OrderType.RecruitSoldier:
                    this.BuyRecruitSoldier();
                    break;
                case OrderType.EquipWithShotgun:
                    this.BuyShotgun(targetSoldier);
                    break;
                case OrderType.EquipWithMachineGun:
                    this.BuyMachineGun(targetSoldier);
                    break;
            }
        }

        private void BuyShotgun(int? targetSoldier)
        {
            if (targetSoldier.HasValue)
            {
                this.Actions.Enqueue(() => AddShotgun(targetSoldier.Value));
            } 
            else
            {
                this.Actions.Enqueue(AddShotgun);
            }
        }

        private void BuyMachineGun(int? targetSoldier)
        {
            if (targetSoldier.HasValue)
            {
                this.Actions.Enqueue(() => AddMachineGun(targetSoldier.Value));
            }
            else
            {
                this.Actions.Enqueue(AddMachineGun);
            }   
        }

        private void BuyRecruitSoldier()
        {
            this.Actions.Enqueue(RecruitSoldier);
        }

        

        private void RecruitSoldier()
        {
            this.Soldiers.Add(new Soldier(this));
        }

        private void AddMachineGun(int id)
        {
            SoliderById(id).Weapon = new MachineGun();
        }

        private void AddMachineGun()
        {
            this.SoldierWithoutWeapon[0].Weapon = new MachineGun();
        }

        private void AddShotgun()
        {
            this.SoldierWithoutWeapon[0].Weapon = new Shotgun();
        }
        private void AddShotgun(int id)
        {
            SoliderById(id).Weapon = new Shotgun();
        }
    }
}
