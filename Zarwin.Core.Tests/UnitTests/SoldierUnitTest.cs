using System;
using Xunit;
using Zarwin.Core.Entity;
using Zarwin.Core.Exceptions;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Tests.UnitTests
{
    public class SoldierUnitTest
    {
        private Soldier soldier;

        ///
        /// Basic Constructor Tests
        ///

        ///<summary>
        /// Test on creation of a solider
        /// </summary>
        [Fact]
        public void InitSoldier()
        {
            soldier = new Soldier(new City());
            Assert.Equal(1, soldier.Level);
            Assert.Equal(4, soldier.HealthPoints);
            Assert.Equal(1, soldier.AttackPoints);
        }

        /// <summary>
        /// Test on creation of two soldiers
        /// </summary>
        [Fact]
        public void InitTwoSoldiers()
        {
            soldier = new Soldier(new City());
            var sold2 = new Soldier(new City());

            Assert.Equal(soldier.Id + 1, sold2.Id);
        }

        ///
        /// Basic Constructor Tests
        ///

        ///<summary>
        /// Test on creation of a soldier with parameter
        /// </summary>
        [Fact]
        public void InitWithParameters()
        {
            soldier = new Soldier(new SoldierParameters(2, 2), new City());
            Assert.Equal(2, soldier.Id);
            Assert.Equal(2, soldier.Level);
            Assert.Equal(5, soldier.HealthPoints);
        }

        /// <summary>
        /// Test on creation of soldier with wrong parameter
        /// </summary>
        [Fact]
        public void InitWithWrongParameters()
        {
            Assert.Throws<WrongParameterException>(() => new Soldier(new SoldierParameters(-1, -1), new City()));
        }

        ///
        ///Tests on Hurt() method
        ///

        ///<summary>
        /// Test on damaging a soldier and kill him
        /// </summary>
        [Fact]
        public void HurtMoreThanHealth()
        {
            soldier = new Soldier(new City());
            soldier.Hurt(soldier.HealthPoints + 1);
            Assert.Equal(0, soldier.HealthPoints);
        }

        /// <summary>
        /// Test on damaging a soldier with 1 damage
        /// </summary>
        [Fact]
        public void HurtOneDamage()
        {
            soldier = new Soldier(new City());
            int health = soldier.HealthPoints;
            soldier.Hurt(1);
            Assert.Equal(health - 1, soldier.HealthPoints);
        }

        /// <summary>
        /// Test on damaging a soldier with multiple damage
        /// </summary>
        [Fact]
        public void HurtMultipleDamage()
        {
            soldier = new Soldier(new City());
            int health = soldier.HealthPoints;
            soldier.Hurt(health - 1);
            Assert.Equal(1, soldier.HealthPoints);
        }


        ///
        ///Tests on levelUp() method
        ///

        ///<summary>
        /// Test on a level up of a soldier
        /// </summary>
        [Fact]
        public void LevelUpBasic()
        {
            soldier = new Soldier(new City());
            int level = soldier.Level;
            soldier.Hurt(soldier.HealthPoints / 2);
            int health = soldier.HealthPoints;
            soldier.LevelUp();
            Assert.Equal(health + 1, soldier.HealthPoints);
            Assert.Equal(level + 1, soldier.Level);
        }

        /// <summary>
        /// Test on a level up of soldier and his health
        /// </summary>
        [Fact]
        public void LevelUpWithMaxHealthPoints()
        {
            soldier = new Soldier(new City());
            soldier.LevelUp();
            Assert.Equal(soldier.Level + 3, soldier.HealthPoints);
        }

        ///
        /// Tests on Attack Points
        ///
        

        ///<summary>
        /// Test with multiple input on the attack of a soldier based on his level
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(21)]
        public void VerifyAttackPoints(int level)
        {
            soldier = new Soldier(new SoldierParameters(0,level), new City());
            Assert.Equal((int)(1 + Math.Floor((decimal)(soldier.Level-1) / 10)), soldier.AttackPoints);
        }
    }
}
