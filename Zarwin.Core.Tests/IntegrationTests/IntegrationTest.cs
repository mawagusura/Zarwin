using Zarwin.Shared.Contracts;
using Zarwin.Shared.Tests;
using Zarwin.Core.Engine;
using Xunit;

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
        /// Run Integration test
        /// </summary>
        [Fact]
        public void IntTest()
        {
            new IntegrationTest();
        }
    }
}
