using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Zarwin.Shared.Contracts;
using Zarwin.Shared.Contracts.Input;
using Zarwin.Shared.Contracts.Output;

namespace Zarwin.Shared.Tests
{
    public abstract partial class IntegratedTests
    {
        public abstract IInstantSimulator CreateSimulator();

        [Theory]
        [MemberData(nameof(ScenarioData), DisableDiscoveryEnumeration = false)]
        public void AllScenario(Scenario scenario)
        {
            scenario.Run(CreateSimulator());
        }

        public static object[][] ScenarioData => Scenarios.Select(s => new object[] { s }).ToArray();

        public class Scenario : IXunitSerializable
        {
            private string name;

            public override string ToString()
            {
                return name;
            }

            private Parameters input;
            private Result expectedOuput;

            public Scenario(string name, Parameters input, Result expectedOuput)
            {
                this.name = name;
                this.input = input;
                this.expectedOuput = expectedOuput;
            }

            public void Run(IInstantSimulator simulator)
            {
                if (input == null || expectedOuput == null)
                    throw new InvalidOperationException("Cannot execute a scenario without input or expectedOutput");

                var actualOutput = simulator.Run(input);

                Assert.Equal(expectedOuput, actualOutput);
            }

            #region IXunitSerializable

            [Obsolete("Shouldn't call this directly", true)]
            public Scenario()
            {
            }

            public void Serialize(IXunitSerializationInfo info)
            {
                info.AddValue("name", name);
                info.AddValue("input", JsonConvert.SerializeObject(input));
                info.AddValue("expectedOutput", JsonConvert.SerializeObject(expectedOuput));
            }

            public void Deserialize(IXunitSerializationInfo info)
            {
                this.name = info.GetValue<string>("name");

                var jsonInput = info.GetValue<string>("input");
                var jsonExpectedOutput = info.GetValue<string>("expectedOutput");
                
                this.input = JsonConvert.DeserializeObject<Parameters>(jsonInput);
                this.expectedOuput = JsonConvert.DeserializeObject<Result>(jsonExpectedOutput);
            }

            #endregion IXunitSerializable
        }
    }
}
