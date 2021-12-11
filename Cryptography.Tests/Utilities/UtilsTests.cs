﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Utilities
{
    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void PositiveModulo_PositiveArguments_ShouldBehaveAsClassicModulo()
        {
            Assert.AreEqual(0, Utils.PositiveModulo(0, 26)); // 0 % 26 = 0
            Assert.AreEqual(1, Utils.PositiveModulo(1, 26)); // 1 % 26 = 1
            Assert.AreEqual(6, Utils.PositiveModulo(6, 26)); // 6 % 26 = 6
            Assert.AreEqual(25, Utils.PositiveModulo(25, 26)); // 25 % 26 = 25

            Assert.AreEqual(0, Utils.PositiveModulo(26, 26)); // 26 % 26 = 0
            Assert.AreEqual(1, Utils.PositiveModulo(27, 26)); // 27 % 26 = 1
            Assert.AreEqual(25, Utils.PositiveModulo(51, 26)); // 51 % 26 = 25
            Assert.AreEqual(0, Utils.PositiveModulo(52, 26)); // 52 % 26 = 0
        }

        [Test]
        public void PositiveModulo_NegativeAPositiveB_ShouldReturnsOnlyPositiveResults()
        {
            Assert.AreEqual(25, Utils.PositiveModulo(-1, 26)); // 25 % 26 = 25
            Assert.AreEqual(20, Utils.PositiveModulo(-6, 26)); // 20 % 26 = 20
            Assert.AreEqual(1, Utils.PositiveModulo(-25, 26)); // 1 % 26 = 1

            Assert.AreEqual(0, Utils.PositiveModulo(-26, 26)); // 0 % 26 = 0
            Assert.AreEqual(25, Utils.PositiveModulo(-27, 26)); // 25 % 26 = 25
            Assert.AreEqual(1, Utils.PositiveModulo(-51, 26)); // 1 % 26 = 1
            Assert.AreEqual(0, Utils.PositiveModulo(-52, 26)); // 0 % 26 = 0
        }
    }
}