using System.Collections.Generic;
using Xunit;
using Zarwin.Core.Entity;
using Zarwin.Core.Entity.SoldierWeapon;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Tests.UnitTests
{
    public class CityUnitTest
    {
        private City city;


        ///
        /// Constructor tests
        ///


        /// <summary>
        /// Test on creation of a city with parameter
        /// </summary>
        [Fact]
        public void InitCityWithParameters()
        {

            List<SoldierParameters> parameters = new List<SoldierParameters>() { new SoldierParameters(1, 1) };
            Squad squad= new Squad(parameters);
            city = new City(new CityParameters(5), squad);
            Assert.Equal(5, city.GetWall().HealthPoints);
            Assert.NotEmpty(city.GetSquad().SoldiersAlive);
        }

        ///
        /// Tests on HurtWall() method
        ///

        /// <summary>
        /// Test on damaging the wall and destroy it
        /// </summary>
        [Fact]
        public void HurtWallMoreThanHealth()
        {
            city = new City();
            city.GetWall().Hurt(city.GetWall().HealthPoints+ 1);
            Assert.Equal(0, city.GetWall().HealthPoints);
        }

        /// <summary>
        /// Test on damaging the wall with one damage
        /// </summary>
        [Fact]
        public void HurtWallOneDamage()
        {
            city = new City();
            int health = city.GetWall().HealthPoints;
            city.GetWall().Hurt(1);
            Assert.Equal(health - 1, city.GetWall().HealthPoints);
        }


        /// <summary>
        /// Test on damaging the wall with multiple damage
        /// </summary>
        [Fact]
        public void HurtWallMultipleDamage()
        {
            city = new City();
            int health = city.GetWall().HealthPoints ;
            city.GetWall().Hurt(health - 1);
            Assert.Equal(1, city.GetWall().HealthPoints);
        }

        [Fact]
        public void BuyMachineGun()
        {
            List<SoldierParameters> param = new List<SoldierParameters>
            {
                new SoldierParameters(1, 1)
            };
            Squad squad = new Squad(param);
            City city = new City(new CityParameters(10),squad);
           
            city.IncreaseMoney(10);
            city.ExecuteOrder(OrderType.EquipWithMachineGun,null);
            city.ExecuteActions();
            Assert.Equal(typeof(MachineGun), city.GetSquad().SoldiersAlive[0].GetWeapon().GetType());
        }

        [Fact]
        public void BuyShotgun()
        {
            List<SoldierParameters> param = new List<SoldierParameters>
            {
                new SoldierParameters(1, 1)
            };
            Squad squad = new Squad(param);
            City city = new City(new CityParameters(10), squad);

            city.IncreaseMoney(10);
            city.ExecuteOrder(OrderType.EquipWithShotgun, null);
            city.ExecuteActions();
            Assert.Equal(typeof(Shotgun), city.GetSquad().SoldiersAlive[0].GetWeapon().GetType()); 
        }

        [Fact]
        public void NotEnoughMoney()
        {
            List<SoldierParameters> param = new List<SoldierParameters>
            {
                new SoldierParameters(1, 1)
            };
            Squad squad = new Squad(param);
            City city = new City(new CityParameters(10), squad);

            city.ExecuteOrder(OrderType.EquipWithMachineGun, null);
            city.ExecuteActions();
            Assert.Equal(typeof(Hand), city.GetSquad().SoldiersAlive[0].GetWeapon().GetType());
        }
        
    }
}
