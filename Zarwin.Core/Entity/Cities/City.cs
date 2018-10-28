using System;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity.Squads;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Entity.Cities
{
    public class City
    {
        private const int PriceUpgrade = 10;

        public Wall Wall { get; }
        public OrderHandler OrderHandler { get; }
        public Squad Squad { get; } 
        public UserInterface UserInterface { get; }


        public int Money { get; private set;  }

        public City() : this(new CityParameters(10), new SoldierParameters[0], new UserInterface(false)) { }

        public City(CityParameters cityParameter,SoldierParameters[] soldierParameters,UserInterface userInterface)
        {
            this.Wall =new Wall(cityParameter.WallHealthPoints);
            this.Money = cityParameter.InitialMoney;
            this.UserInterface = userInterface;
            this.OrderHandler= new OrderHandler(this);
            this.Squad = new Squad(soldierParameters, this);
        }
        
        public void IncreaseMoney(int money)
        {
            this.Money += money;
        }

        public Boolean BuyOrder()
        {
            if (Money < PriceUpgrade)
            {
                return false;
            }
            Money -= PriceUpgrade;
            return true;
        }
        
    }
}
