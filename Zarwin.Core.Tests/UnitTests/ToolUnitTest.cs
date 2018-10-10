using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity;
using Zarwin.Shared.Contracts.Core;

namespace Zarwin.Core.Tests.UnitTests
{
    public class ToolUnitTest
    {
        /// <summary>
        /// Test a selector can select an item from a list given
        /// </summary>
        [Fact]
        public void TestItemSelector()
        {

            ItemSelector selector = new ItemSelector();
            List<int> list = new List<int>
            {
                0,
                3,
                13
            };
            Assert.Contains(selector.SelectItem(list), list);
        }

        /// <summary>
        /// Test on a damage dispatcher and it capability to dispatch all damages given
        /// </summary>
        [Fact]
        public void TestDamageDispatcherDispatchAllDamage()
        {
            DamageDispatcher dispatcher = new DamageDispatcher(new ItemSelector());
            List<ISoldier> soldiers = new List<ISoldier>();
            Soldier s = new Soldier();
            Soldier s2 = new Soldier();
            soldiers.Add(s);
            soldiers.Add(s2);

            int damageToDeal = 6;
            int sumHPInit = soldiers.Sum(soldier => soldier.HealthPoints);
            dispatcher.DispatchDamage(damageToDeal, soldiers);
            Assert.True(soldiers.Sum(soldier => soldier.HealthPoints) == (sumHPInit - damageToDeal) );
        }

        /// <summary>
        /// Test on a damage dispatcher to dispatch the max damage possible
        /// </summary>
        [Fact]
        public void TestDamageDispatcherOverkill()
        {
            DamageDispatcher dispatcher = new DamageDispatcher(new ItemSelector());
            List<ISoldier> soldiers = new List<ISoldier>();
            Soldier s = new Soldier();
            soldiers.Add(s);
            Assert.True(s.HealthPoints == 4);

            dispatcher.DispatchDamage(s.HealthPoints + 1, soldiers);
            Assert.True(s.HealthPoints == 0);
        }

        /// <summary>
        /// Test on the UserInterface and it capacity to read a message
        /// </summary>
        [Fact]
        public void TestInput()
        {
            var input = new StringReader("Test");
            Console.SetIn(input);
            Assert.Equal("Test", UserInterface.ReadMessage(true));
        }

        /// <summary>
        ///  Test on the UserInterface and the capacity to print a message
        /// </summary>
        [Fact]
        public void TestOutput()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            UserInterface.PrintMessage("Test", true);
            Assert.Equal("Test\r\n", output.ToString());
        }
    }
}
