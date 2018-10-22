using System.Collections.Generic;
using Xunit;
using Zarwin.Core.Entity;
using Zarwin.Core.Entity.Weapon;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Tests.UnitTests
{
    public class CityUnitTest
    {
        private City city;


        ///
        /// Constructor tests
        ///

        ///<summary>
        /// Test on creation of a city without parameter
        ///</summary>
        [Fact]
        public void InitCity()
        {
            city = new City();
            Assert.NotNull(city.Soldiers);
            Assert.Equal(10, city.WallHealthPoints);
        }

        /// <summary>
        /// Test on creation of a city with parameter
        /// </summary>
        [Fact]
        public void InitCityWithParameters()
        {
            List<SoldierParameters> parameters = new List<SoldierParameters>() { new SoldierParameters(1, 1) };
            city = new City(new CityParameters(5), parameters);
            Assert.Equal(5, city.WallHealthPoints);
            Assert.NotEmpty(city.Soldiers);
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
            city.HurtWall(city.WallHealthPoints + 1);
            Assert.Equal(0, city.WallHealthPoints);
        }

        /// <summary>
        /// Test on damaging the wall with one damage
        /// </summary>
        [Fact]
        public void HurtWallOneDamage()
        {
            city = new City();
            int health = city.WallHealthPoints;
            city.HurtWall(1);
            Assert.Equal(health - 1, city.WallHealthPoints);
        }


        /// <summary>
        /// Test on damaging the wall with multiple damage
        /// </summary>
        [Fact]
        public void HurtWallMultipleDamage()
        {
            city = new City();
            int health = city.WallHealthPoints;
            city.HurtWall(health - 1);
            Assert.Equal(1, city.WallHealthPoints);
        }

        [Fact]
        public void BuyMachineGun()
        {
            city = new City();
            city.Soldiers.Add(new Soldier(city));
            city.IncreaseMoney(10);
            city.ExecuteOrder(OrderType.EquipWithMachineGun,null);
            city.ExecuteActions();
            Assert.True(city.Soldiers[0].Weapon.GetType() == typeof(MachineGun));
        }

        [Fact]
        public void BuyShotgun()
        {
            city = new City();
            city.Soldiers.Add(new Soldier(city));
            city.IncreaseMoney(10);
            city.ExecuteOrder(OrderType.EquipWithShotgun, null);
            city.ExecuteActions();
            Assert.True(city.Soldiers[0].Weapon.GetType() == typeof(Shotgun));
        }

        [Fact]
        public void NotEnoughMoney()
        {
            city = new City();
            city.Soldiers.Add(new Soldier(city));
            city.ExecuteOrder(OrderType.EquipWithMachineGun, null);
            city.ExecuteActions();
            Assert.Equal(typeof(Hand),city.Soldiers[0].Weapon.GetType());
        }
        
    }
}
