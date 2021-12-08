using System;
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

        [Test]
        public void GeneratePermutationSeries_SeriesLength3AndUseNumberCount2_ShouldReturnCorrectly()
        {
            var list = Utils.GeneratePermutationSeries(seriesLength: 3, useNumberCount: 2);

            List<List<int>> expected = new()
            {
                new() {0, 0, 0},
                new() {1, 0, 0},
                new() {0, 1, 0},
                new() {1, 1, 0},
                new() {0, 0, 1},
                new() {1, 0, 1},
                new() {0, 1, 1},
                new() {1, 1, 1},
            };

            Assert.That(list, Is.EquivalentTo(expected));
        }

        [Test]
        public void GeneratePermutationSeries_SeriesLength4AndUseNumberCount3_ShouldReturnCorrectly()
        {
            var list = Utils.GeneratePermutationSeries(4, 3);

            List<List<int>> expected = new()
            {
                new() {0, 0, 0, 0},
                new() {1, 0, 0, 0},
                new() {2, 0, 0, 0},

                new() {0, 1, 0, 0},
                new() {1, 1, 0, 0},
                new() {2, 1, 0, 0},

                new() {0, 2, 0, 0},
                new() {1, 2, 0, 0},
                new() {2, 2, 0, 0},


                new() {0, 0, 1, 0},
                new() {1, 0, 1, 0},
                new() {2, 0, 1, 0},

                new() {0, 1, 1, 0},
                new() {1, 1, 1, 0},
                new() {2, 1, 1, 0},

                new() {0, 2, 1, 0},
                new() {1, 2, 1, 0},
                new() {2, 2, 1, 0},

                new() {0, 0, 2, 0},
                new() {1, 0, 2, 0},
                new() {2, 0, 2, 0},

                new() {0, 1, 2, 0},
                new() {1, 1, 2, 0},
                new() {2, 1, 2, 0},

                new() {0, 2, 2, 0},
                new() {1, 2, 2, 0},
                new() {2, 2, 2, 0},


                new() {0, 0, 0, 1},
            };

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(list.Take(28), Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(list.Count(), Is.EqualTo(Math.Pow(3, 4)));
        }
    }
}