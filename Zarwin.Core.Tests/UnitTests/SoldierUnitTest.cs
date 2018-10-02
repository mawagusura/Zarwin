﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Zarwin.Core.entity;

namespace Zarwin.Core.Tests.UnitTests
{
    public class SoldierUnitTest
    {
        private readonly Soldier soldier;

        public SoldierUnitTest()
        {
            soldier = new Soldier();
        }

        /// <summary>
        /// Hurt more than solider
        /// </summary>
        [Fact]
        public void hurtMoreThanHealth()
        {
            soldier.Hurt(soldier.HealthPoints+1);
            Assert.False(soldier.Alive);
        }
    }
}