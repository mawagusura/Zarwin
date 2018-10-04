using Zarwin.Shared.Contracts;
using Zarwin.Shared.Tests;
using Zarwin.Core.Engine;

namespace Zarwin.Core.Tests.IntegrationTests
{
    class IntegrationTest : IntegratedTests
    {
        public override IInstantSimulator CreateSimulator()
        {
            return new Simulator();
        }
    }
}
