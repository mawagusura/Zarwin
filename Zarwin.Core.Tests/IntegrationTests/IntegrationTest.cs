using Zarwin.Shared.Contracts;
using Zarwin.Shared.Tests;
using Zarwin.Core.Engine;
using Xunit;

namespace Zarwin.Core.Tests.IntegrationTests
{
    public class IntegrationTest : IntegratedTests
    {
        public override IInstantSimulator CreateSimulator()
        {
            return new Simulator();
        }

        [Fact]
        public void IntTest()
        {
            new IntegrationTest();
        }
    }
}
