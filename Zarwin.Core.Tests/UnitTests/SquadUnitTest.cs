using System;
using Xunit;
using Zarwin.Core.Entity.Cities;
using Zarwin.Core.Entity.Squads;
using Zarwin.Core.Entity.Squads.Weapons;
using Zarwin.Core.Exceptions;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Tests.UnitTests
{
    public class SquadUnitTest
    {

        ///
        /// Basic Constructor Tests
        ///
        
        [Fact]
        public void InitSquad()
        {
            Squad squad = new Squad();
            squad.RecruitSoldier();
            Soldier soldier = squad.SoldiersAlive[0];
            Assert.Equal(1, soldier.Id);
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
            Squad squad = new Squad();
            squad.RecruitSoldier();
            squad.RecruitSoldier();
            Assert.Equal(squad.SoldiersAlive[0].Id + 1, squad.SoldiersAlive[1].Id);
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
            SoldierParameters[] soldierParameters = { new SoldierParameters(2, 2) };
            Squad squad = new Squad(soldierParameters);
            Soldier soldier = squad.SoldiersAlive[0];
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
            Assert.Throws<WrongParameterException>(() => new Soldier(new SoldierParameters(-1, -1),null));
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
            Squad squad = new Squad();
            squad.RecruitSoldier();
            Soldier soldier = squad.SoldiersAlive[0];
            soldier.Hurt(soldier.HealthPoints + 1);
            Assert.Equal(0, soldier.HealthPoints);
        }

        /// <summary>
        /// Test on damaging a soldier with 1 damage
        /// </summary>
        [Fact]
        public void HurtOneDamage()
        {
            Squad squad = new Squad();
            squad.RecruitSoldier();
            Soldier soldier = squad.SoldiersAlive[0];
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
            Squad squad = new Squad();
            squad.RecruitSoldier();
            Soldier soldier = squad.SoldiersAlive[0];
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
            Squad squad = new Squad();
            squad.RecruitSoldier();
            Soldier soldier = squad.SoldiersAlive[0];
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
            Squad squad = new Squad();
            squad.RecruitSoldier();
            Soldier soldier = squad.SoldiersAlive[0];
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
            SoldierParameters[] soldierParameters = { new SoldierParameters(0, level) };
            Squad squad = new Squad(soldierParameters);
            squad.RecruitSoldier();
            Soldier soldier = squad.SoldiersAlive[0];
            Assert.Equal((int)(1 + Math.Floor((decimal)(soldier.Level-1) / 10)), soldier.AttackPoints);
        }

        [Fact]
        public void VerifyAttackShotgunWall()
        {
            Shotgun shotgun = new Shotgun(new Wall(10));
            Assert.True(1==shotgun.AttackMultiplier());
        }

        [Fact]
        public void VerifyAttackShotgunNoWall()
        {
            Shotgun shotgun = new Shotgun(new Wall(0));
            Assert.True(2 == shotgun.AttackMultiplier());
        }

        [Fact]
        public void VerifyAttackMachineGunWall()
        {
            MachineGun machineGun= new MachineGun(new Wall(10));
            Assert.True(4 == machineGun.AttackMultiplier());
        }

        [Fact]
        public void VerifyAttackMachineGunNoWall()
        {
            MachineGun machineGun = new MachineGun(new Wall(0));
            Assert.True(1 == machineGun.AttackMultiplier()); 
        }

        [Fact]
        public void VerifyHandWall()
        {
            Hand hand= new Hand(new Wall(10));
            Assert.True(1 == hand.AttackMultiplier());
        }

        [Fact]
        public void VerifyHandNoWall()
        {
            Hand hand = new Hand(new Wall(0));
            Assert.True(1 == hand.AttackMultiplier());
        }
    }
}
