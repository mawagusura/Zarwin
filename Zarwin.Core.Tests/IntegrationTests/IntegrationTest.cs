using Zarwin.Shared.Contracts;
using Zarwin.Shared.Tests;
using Zarwin.Core.Engine;
using Xunit;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Core.Tests.IntegrationTests
{
    public class IntegrationTest : IntegratedTests
    {
        /// <summary>
        /// Create a simulation without player (false)
        /// </summary>
        /// <returns></returns>
        public override IInstantSimulator CreateSimulator()
        {
            return new Simulator(false);
        }

        /// <summary>
        /// Create a fake test, so that visual studio can detect test
        /// from Shared.test
        /// </summary>
        [Fact]
        public void WallTakeDamage()
        {
           Parameters input = new Parameters(
                1,
                new FirstSoldierDamageDispatcher(),
                new HordeParameters(3),
                new CityParameters(2),
                new SoldierParameters(1, 1));
            Result actualOutput = CreateSimulator().Run(input);

            Assert.Equal(0, actualOutput.Waves[0].Turns[1].WallHealthPoints);
        }

        [Fact]
        public void SoldierStompHorde()
        {
            Parameters input = new Parameters(
                1,
                new FirstSoldierDamageDispatcher(),
                new HordeParameters(10),
                new CityParameters(0),
                new SoldierParameters(1, 200));

            Result actualOutput = CreateSimulator().Run(input);
            Assert.Equal(0, actualOutput.Waves[0].Turns[1].Horde.Size);

        }

        [Fact]
        public void HordeStompSoldier()
        {
            Parameters input = new Parameters(
                2,
                new FirstSoldierDamageDispatcher(),
                new HordeParameters(10),
                new CityParameters(0),
                new SoldierParameters(1, 1));

            Result actualOutput = CreateSimulator().Run(input);
            Assert.Equal(0, actualOutput.Waves[0].Turns[1].Soldiers[0].HealthPoints);

        }


    }
}
