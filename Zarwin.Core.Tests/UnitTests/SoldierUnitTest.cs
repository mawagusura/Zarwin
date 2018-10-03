using System;
using System.Collections.Generic;
using System.Text;
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

        [Fact]
        public void InitSoldier()
        {
            soldier = new Soldier();
            Assert.Equal(1, soldier.Level);
            Assert.Equal(4, soldier.HealthPoints);
            Assert.Equal(1, soldier.AttackPoints);
        }

        [Fact]
        public void InitTwoSoldiers()
        {
            soldier = new Soldier();
            var sold2 = new Soldier();

            Assert.Equal(soldier.Id + 1, sold2.Id);
        }

        ///
        /// Basic Constructor Tests
        ///

        [Fact]
        public void InitWithParameters()
        {
            soldier = new Soldier(new SoldierParameters(2, 2));
            Assert.Equal(2, soldier.Id);
            Assert.Equal(2, soldier.Level);
        }

        [Fact]
        public void InitWithWrongParameters()
        {
            soldier = new Soldier(new Shared.Contracts.Input.SoldierParameters(2, 2));
            Assert.Throws<WrongParameterException>(() => soldier = new Soldier(new SoldierParameters(-1, -1)));
        }

        ///
        ///Tests on Hurt() method
        ///

        [Fact]
        public void HurtMoreThanHealth()
        {
            soldier = new Soldier();
            soldier.Hurt(soldier.HealthPoints + 1);
            Assert.Equal(0, soldier.HealthPoints);
        }

        [Fact]
        public void HurtOneDamage()
        {
            soldier = new Soldier();
            int health = soldier.HealthPoints;
            soldier.Hurt(1);
            Assert.Equal(health - 1, soldier.HealthPoints);
        }

        [Fact]
        public void HurtMultipleDamage()
        {
            soldier = new Soldier();
            int health = soldier.HealthPoints;
            soldier.Hurt(health - 1);
            Assert.Equal(1, soldier.HealthPoints);
        }


        ///
        ///Tests on levelUp() method
        ///

        [Fact]
        public void LevelUpBasic()
        {
            soldier = new Soldier();
            int level = soldier.Level;
            soldier.Hurt(soldier.HealthPoints / 2);
            int health = soldier.HealthPoints;
            soldier.LevelUp();
            Assert.Equal(health + 1, soldier.HealthPoints);
            Assert.Equal(level + 1, soldier.Level);
        }

        [Fact]
        public void LevelUpWithMaxHealthPoints()
        {
            soldier = new Soldier();
            soldier.LevelUp();
            Assert.Equal(soldier.Level + 3, soldier.HealthPoints);
        }

        ///
        /// Tests on Attack Points
        ///
        
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(21)]
        public void VerifyAttackPoints(int level)
        {
            soldier = new Soldier();
            Assert.Equal((int)(1 + Math.Floor((decimal)(soldier.Level) / 10)), soldier.AttackPoints);
        }
    }
}
