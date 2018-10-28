using System;
using System.Collections.Generic;
using Zarwin.Core.Entity.Squads.Weapons;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity.Cities
{
    public class OrderHandler
    {
        private City City{ get; }
        public Queue<Action> Orders { get; } = new Queue<Action>();

        public OrderHandler(City city)
        {
            this.City = city;
        }
        public void ExecuteOrders()
        {
            while (this.Orders.Count > 0)
            {
                this.Orders.Dequeue()();
            }
        }


        public void BuyOrders(IEnumerable<Order> orders)
        {
            foreach(Order order in orders)
            {
                BuyOrders(order);
            }
        }

        public void BuyOrders(Order order)
        {
            if (this.City.BuyOrder())
            {
                this.AddOrder(order.Type, this.City.Wall, order.TargetSoldier);
            }
        }

        private void AddOrder(OrderType orderType, Wall wall, int? targetSoldier)
        {
            switch (orderType)
            {
                case OrderType.RecruitSoldier:
                    this.OrderRecruitSoldier();
                    break;
                case OrderType.EquipWithShotgun:
                    this.OrderShotgun(targetSoldier,wall);
                    break;
                case OrderType.EquipWithMachineGun:
                    this.OrderMachineGun(targetSoldier,wall);
                    break;
            }
        }

        private void OrderShotgun(int? targetSoldier,Wall wall)
        {
            if (targetSoldier.HasValue)
            {
                this.Orders.Enqueue(() => AddShotgun(targetSoldier.Value, wall));
            }
            else
            {
                this.Orders.Enqueue(() => AddShotgun(wall));
            }
        }

        private void OrderMachineGun(int? targetSoldier,Wall wall)
        {
            if (targetSoldier.HasValue)
            {
                this.Orders.Enqueue(() => AddMachineGun(targetSoldier.Value,wall));
            }
            else
            {
                this.Orders.Enqueue(() => AddMachineGun(wall));
            }
        }

        private void OrderRecruitSoldier()
        {
            this.Orders.Enqueue(this.City.Squad.RecruitSoldier);
        }

        private void AddMachineGun(int id,Wall wall)
        {
            this.City.Squad.SoliderById(id).SetWeapon(new MachineGun(wall));
        }

        private void AddMachineGun(Wall wall)
        {
            this.City.Squad.SoldiersWithoutWeapon[0].SetWeapon(new MachineGun(wall));
        }

        private void AddShotgun(Wall wall)
        {
            this.City.Squad.SoldiersWithoutWeapon[0].SetWeapon(new Shotgun(wall));
        }
        private void AddShotgun(int id,Wall wall)
        {
            this.City.Squad.SoliderById(id).SetWeapon(new Shotgun(wall));
        }
    }
}
