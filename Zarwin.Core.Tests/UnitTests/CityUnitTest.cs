using System.Collections.Generic;
using Xunit;
using Zarwin.Core.Entity.Cities;
using Zarwin.Core.Entity.Squads;
using Zarwin.Core.Entity.Squads.Weapons;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Tests.UnitTests
{
    public class CityUnitTest
    {
        ///
        /// Constructor tests
        ///


        /// <summary>
        /// Test on creation of a city with parameter
        /// </summary>
        [Fact]
        public void InitCityWithParameters()
        {
            SoldierParameters[] soldierParameters = {new SoldierParameters(13, 5) };
            City city = new City(new CityParameters(5,3), soldierParameters, new Engine.Tool.UserInterface(false));
            Assert.Equal(5, city.Wall.HealthPoints);
            Assert.Equal(3, city.Money);
            Assert.Equal(13, city.Squad.SoldiersAlive[0].Id);
            Assert.Equal(5, city.Squad.SoldiersAlive[0].Level);
        }

        ///
        /// Tests on Wall Hurt() method
        ///

        /// <summary>
        /// Test on damaging the wall and destroy it
        /// </summary>
        [Fact]
        public void HurtWallMoreThanHealth()
        {
            City city = new City();
            city.Wall.Hurt(city.Wall.HealthPoints+ 1);
            Assert.Equal(0, city.Wall.HealthPoints);
        }

        /// <summary>
        /// Test on damaging the wall with one damage
        /// </summary>
        [Fact]
        public void HurtWallOneDamage()
        {
            City city = new City();
            int health = city.Wall.HealthPoints;
            city.Wall.Hurt(1);
            Assert.Equal(health - 1, city.Wall.HealthPoints);
        }


        /// <summary>
        /// Test on damaging the wall with multiple damage
        /// </summary>
        [Fact]
        public void HurtWallMultipleDamage()
        {
            City city = new City();
            int health = city.Wall.HealthPoints ;
            city.Wall.Hurt(health - 1);
            Assert.Equal(1, city.Wall.HealthPoints);
        }
        
        [Fact]
        public void BuyOrder()
        {
            City city = new City();
            city.IncreaseMoney(15);
            Assert.True(city.BuyOrder());
            Assert.Equal(5, city.Money);
        }

        [Fact]
        public void CanNotBuyOrder()
        {
            City city = new City();
            city.IncreaseMoney(5);
            Assert.False(city.BuyOrder());
            Assert.Equal(5, city.Money);
        }

        [Fact]
        public void BuyRecruiteSoldier()
        {
            City city = new City();
            city.IncreaseMoney(10);


            city.OrderHandler.BuyOrders(new Order(0, 0, OrderType.RecruitSoldier));
            city.OrderHandler.ExecuteOrders();

            Assert.Single(city.Squad.SoldiersAlive);
        }

        [Fact]
        public void BuyMachineGun()
        {
            City city = new City();
            city.Squad.RecruitSoldier();
            city.IncreaseMoney(10);


            city.OrderHandler.BuyOrders(new Order(0, 0, OrderType.EquipWithMachineGun));
            city.OrderHandler.ExecuteOrders();

            Assert.IsType<MachineGun>(city.Squad.SoldiersAlive[0].Weapon);
            Assert.Equal(0, city.Money);
        }

        [Fact]
        public void BuyShotgun()
        {
            City city = new City();
            city.Squad.RecruitSoldier();
            city.IncreaseMoney(10);


            city.OrderHandler.BuyOrders(new Order(0, 0, OrderType.EquipWithShotgun));
            city.OrderHandler.ExecuteOrders();

            Assert.IsType<Shotgun>(city.Squad.SoldiersAlive[0].Weapon);
            Assert.Equal(0, city.Money);
        }
    }
}
