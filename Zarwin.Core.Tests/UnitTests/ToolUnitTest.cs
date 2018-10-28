using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Zarwin.Core.Engine.Tool;
using Zarwin.Core.Entity.Squads;
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
            Squad squad = new Squad();
            DamageDispatcher dispatcher = new DamageDispatcher(new ItemSelector());
            squad.RecruitSoldier();
            squad.RecruitSoldier();

            int damageToDeal = 6;
            int sumHPInit = squad.SoldiersAlive.Sum(soldier => soldier.HealthPoints);
            dispatcher.DispatchDamage(damageToDeal, squad.SoldiersAlive);
            Assert.True(squad.SoldiersAlive.Sum(soldier => soldier.HealthPoints) == (sumHPInit - damageToDeal) );
        }

        /// <summary>
        /// Test on a damage dispatcher to dispatch the max damage possible
        /// </summary>
        [Fact]
        public void TestDamageDispatcherOverkill()
        {
            DamageDispatcher dispatcher = new DamageDispatcher(new ItemSelector());
            List<ISoldier> soldiers = new List<ISoldier>();
            Soldier s = new Soldier(1, new Squad());
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
            UserInterface userInterface = new UserInterface(true);
            var input = new StringReader("Test");
            Console.SetIn(input);
            Assert.Equal("Test", userInterface.ReadMessage());
        }

        [Fact]
        public void TestOutputSoldierHit()
        {
            UserInterface userInterface = new UserInterface(true);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeSoliderHit(1, 10);
            Assert.Equal("Soldat #1 a perdu 10 PV." + Environment.NewLine, output.ToString());
        }

        [Fact]
        public void TestNoOutputSoldierHit()
        {
            UserInterface userInterface = new UserInterface(false);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeSoliderHit(1,10);
            Assert.Equal("", output.ToString());
        }

        [Fact]
        public void TestOutputSoldierDown()
        {
            UserInterface userInterface = new UserInterface(true);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeSoliderDown(1);
            Assert.Equal("Soldat #1 est tombé." + Environment.NewLine, output.ToString());
        }

        [Fact]
        public void TestNoOutputSoldierDown()
        {
            UserInterface userInterface = new UserInterface(false);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeSoliderDown(10);
            Assert.Equal("", output.ToString());
        }

        [Fact]
        public void TestOutputSoldierKill()
        {
            UserInterface userInterface = new UserInterface(true);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeSoliderKill(1, 10);
            Assert.Equal("Soldat #1 a tué 10 zombies." + Environment.NewLine, output.ToString());
        }

        [Fact]
        public void TestNoOutputSoldierKill()
        {
            UserInterface userInterface = new UserInterface(false);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeSoliderKill(1, 1);
            Assert.Equal("", output.ToString());
        }

        [Fact]
        public void TestOutputEndTurn()
        {
            UserInterface userInterface = new UserInterface(true);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeEndTurn(10);
            Assert.Equal("Fin du tour. Reste 10 zombies."+ Environment.NewLine, output.ToString());
        }

        [Fact]
        public void TestNoOutputEndTurn()
        {
            UserInterface userInterface = new UserInterface(false);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeEndTurn(10);
            Assert.Equal("", output.ToString());
        }

        [Fact]
        public void TestOutputEndWave()
        {
            UserInterface userInterface = new UserInterface(true);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeEndWave();
            Assert.Equal("Fin de vague." + Environment.NewLine, output.ToString());
        }

        [Fact]
        public void TestNoOutputEndWave()
        {
            UserInterface userInterface = new UserInterface(false);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeEndWave();
            Assert.Equal("", output.ToString());
        }

        [Fact]
        public void TestOutputApproach()
        {
            UserInterface userInterface = new UserInterface(true);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeApproach();
            Assert.Equal("Horde en approche." + Environment.NewLine, output.ToString());
        }

        [Fact]
        public void TestNoOutputApproach()
        {
            UserInterface userInterface = new UserInterface(false);
            var output = new StringWriter();
            Console.SetOut(output);
            userInterface.InvokeApproach();
            Assert.Equal("", output.ToString());
        }
    }
}
