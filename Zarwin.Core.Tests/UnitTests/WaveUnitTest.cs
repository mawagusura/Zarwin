using System.Collections.Generic;
using Xunit;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity.Cities;
using Zarwin.Core.Entity.Waves;
using Zarwin.Shared.Contracts.Input;

namespace Zarwin.Core.Tests.UnitTests
{
    public class WaveUnitTest
    {
        [Fact]
        public void InitWave()
        {
            Wave wave = new Wave();
            Assert.False(wave.Horde.IsAlive);
            Assert.Empty(wave.Orders);
            Assert.Empty(wave.TurnResults);
            Assert.IsType<DamageDispatcher>(wave.DamageDispatcher);
            Assert.NotNull(wave.InitialResult);
            Assert.NotNull(wave.WaveEngine);
            Assert.NotNull(wave.City);
        }

        [Fact]
        public void InitWaveWitParameters()
        {
            ZombieParameter[] zombieParameters =
            {
                new ZombieParameter(ZombieType.Fatty,ZombieTrait.Normal,3)
            };
            List<Order> orders = new List<Order>
            {
                new Order(0, 0, OrderType.RecruitSoldier),
                new Order(0, 0, OrderType.EquipWithShotgun)
            };
            City city = new City();
            DamageDispatcher damageDispatcher = new DamageDispatcher(new ItemSelector());

            Wave wave = new Wave(zombieParameters,city,orders,damageDispatcher);
            
            Assert.True(wave.Horde.IsAlive);
            Assert.Equal(3,wave.Horde.ZombiesAlive.Count);
            Assert.Equal(2,wave.Orders.Count);
            Assert.Empty(wave.TurnResults);
            Assert.Equal(damageDispatcher,wave.DamageDispatcher);
            Assert.Equal(city, wave.City);
            Assert.NotNull(wave.InitialResult);
            Assert.NotNull(wave.WaveEngine);
        }

        [Fact]
        public void InitHordeWithParameters()
        {
            ZombieParameter[] zombieParameters = { new ZombieParameter(ZombieType.Stalker,ZombieTrait.Normal,1) };
            Horde horde = new Horde(zombieParameters);
            Assert.True(horde.IsAlive);
            Assert.Single(horde.ZombiesAlive);
            Assert.Equal(1, horde.ZombiesAlive[0].Id);
            Assert.Equal(1,horde.ZombiesAlive[0].HealthPoints);
            Assert.Equal(ZombieType.Stalker, horde.ZombiesAlive[0].Type);
            Assert.Equal(ZombieTrait.Normal,horde.ZombiesAlive[0].Trait);
        }
        [Fact]
        public void InitHorde2Zombies()
        {
            ZombieParameter[] zombieParameters = { new ZombieParameter(ZombieType.Stalker, ZombieTrait.Normal, 2) };
            Horde horde = new Horde(zombieParameters);
            Assert.True(horde.IsAlive);
            Assert.Equal(2,horde.ZombiesAlive.Count);
            Assert.Equal(1, horde.ZombiesAlive[0].Id);
            Assert.Equal(1, horde.ZombiesAlive[0].HealthPoints);
            Assert.Equal(ZombieType.Stalker, horde.ZombiesAlive[0].Type);
            Assert.Equal(ZombieTrait.Normal, horde.ZombiesAlive[0].Trait);

            Assert.Equal(2, horde.ZombiesAlive[1].Id);
            Assert.Equal(1, horde.ZombiesAlive[1].HealthPoints);
            Assert.Equal(ZombieType.Stalker, horde.ZombiesAlive[1].Type);
            Assert.Equal(ZombieTrait.Normal, horde.ZombiesAlive[1].Trait);
        }

        [Fact]
        public void InitZombieWithParameters()
        {
            Zombie zombie=new Zombie(1, ZombieTrait.Normal,ZombieType.Stalker);
            Assert.Equal(1, zombie.HealthPoints);
            Assert.Equal(1, zombie.Id);
            Assert.Equal(ZombieType.Stalker, zombie.Type);
            Assert.Equal(ZombieTrait.Normal, zombie.Trait);
        }

        [Fact]
        public void HurtAndKillNormal()
        {
            Zombie zombie = new Zombie(1,ZombieTrait.Normal, ZombieType.Stalker);
            zombie.Hurt(1, 0);
            Assert.Equal(0, zombie.HealthPoints);
        }

        [Fact]
        public void HurtTough()
        {
            Zombie zombie = new Zombie(1, ZombieTrait.Tough, ZombieType.Stalker);
            zombie.Hurt(1, 0);
            Assert.Equal(1, zombie.HealthPoints);
        }

        [Fact]
        public void HurtAndKillTough()
        {
            Zombie zombie = new Zombie(1,ZombieTrait.Tough, ZombieType.Stalker);
            zombie.Hurt(1, 0);
            zombie.Hurt(1, 0);
            Assert.Equal(0, zombie.HealthPoints);
        }

        [Fact]
        public void HorderPriority()
        {
            ZombieParameter[] zombieParameters = 
            {
                    new ZombieParameter(ZombieType.Stalker, ZombieTrait.Normal, 1),
                    new ZombieParameter(ZombieType.Fatty, ZombieTrait.Normal, 1),
                    new ZombieParameter(ZombieType.Fatty, ZombieTrait.Tough, 1),
                    new ZombieParameter(ZombieType.Stalker, ZombieTrait.Tough, 1)
            };
            Horde horde = new Horde(zombieParameters);
            Assert.True(horde.IsAlive);
            Assert.Equal(4,horde.ZombiesAlive.Count);
            Assert.Equal(3, horde.ZombiesAlive[0].Id);
            Assert.Equal(2, horde.ZombiesAlive[1].Id);
            Assert.Equal(4, horde.ZombiesAlive[2].Id);
            Assert.Equal(1, horde.ZombiesAlive[3].Id);
        }
    }
}
