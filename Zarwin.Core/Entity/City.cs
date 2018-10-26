using System;
using System.Collections.Generic;
using System.Linq;
using Zarwin.Core.Entity.SoldierWeapon;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Entity
{
    public class City
    {
        private const int PriceUpgrade = 10;

        public int Money { get; set;  } = 0;

        public Queue<Action> Actions { get; } = new Queue<Action>();

        public Squad Squad { get; }

        public Wall Wall { get; } = new Wall();

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
        public City(CityParameters cityParameter, Squad squad)
        {
            this.Squad = squad;
            this.Wall =new Wall(cityParameter.WallHealthPoints);
            this.Money = cityParameter.InitialMoney;
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
            if (Money < PriceUpgrade)
            {
                return;
            }

            Money -= PriceUpgrade;

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
            this.Actions.Enqueue(this.Squad.RecruitSoldier);
        }


        private void AddMachineGun(int id)
        {
            this.Squad.SoliderById(id).SetWeapon(new MachineGun(this.Wall));
        }

        private void AddMachineGun()
        {
            this.Squad.SoldiersWithoutWeapon[0].SetWeapon(new MachineGun(this.Wall));
        }

        private void AddShotgun()
        {
            this.Squad.SoldiersWithoutWeapon[0].SetWeapon(new Shotgun(this.Wall));
        }
        private void AddShotgun(int id)
        {
            this.Squad.SoliderById(id).SetWeapon(new Shotgun(this.Wall));
        }

    }
}
