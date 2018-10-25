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

        private readonly Wall wall = new Wall();

        public int Money { get; set;  } = 0;

        public Queue<Action> Actions { get; } = new Queue<Action>();

        private readonly Squad squad;

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
            this.squad = squad;
            this.wall =new Wall(cityParameter.WallHealthPoints);
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
            this.Actions.Enqueue(this.squad.RecruitSoldier);
        }


        private void AddMachineGun(int id)
        {
            this.squad.SoliderById(id).SetWeapon(new MachineGun(this.wall));
        }

        private void AddMachineGun()
        {
            this.squad.SoldiersWithoutWeapon[0].SetWeapon(new MachineGun(this.wall));
        }

        private void AddShotgun()
        {
            this.squad.SoldiersWithoutWeapon[0].SetWeapon(new Shotgun(this.wall));
        }
        private void AddShotgun(int id)
        {
            this.squad.SoliderById(id).SetWeapon(new Shotgun(this.wall));
        }

        public Wall GetWall()
        {
            return this.wall;
        }

        public Squad GetSquad()
        {
            return this.squad;
        }
    }
}
