using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Utilities
{
    public class PermutationsTests
    {
        [Test]
        public void GeneratePermutationSeries_SeriesLength3AndUseNumberCount2_ShouldReturnCorrectly()
        {
            var enumerable = Permutations.GeneratePermutationSeries(3, 2);

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

            Assert.That(enumerable, Is.EquivalentTo(expected));
        }

        [Test]
        public void GeneratePermutationSeries_SeriesLength4AndUseNumberCount3_ShouldReturnCorrectly()
        {
            var enumerable = Permutations.GeneratePermutationSeries(4, 3);

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
            Assert.That(enumerable.Take(28), Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Count(), Is.EqualTo(Math.Pow(3, 4)));
        }


        [Test]
        public void GenerateAlphabetPermutations_WordLength3AlphabetLength4_ShouldReturn64Elements()
        {
            // ReSharper disable once StringLiteralTypo
            var enumerable = Permutations.GenerateAlphabetPermutations(3, 0, 4, "abcd");

            char[][] expected =
            {
                new[] {'a', 'a', 'a'},
                new[] {'a', 'a', 'b'},
                new[] {'a', 'a', 'c'},
                new[] {'a', 'a', 'd'},

                new[] {'a', 'b', 'a'},
                new[] {'a', 'b', 'b'},
                new[] {'a', 'b', 'c'},
                new[] {'a', 'b', 'd'},

                new[] {'a', 'c', 'a'},
                new[] {'a', 'c', 'b'},
                new[] {'a', 'c', 'c'},
                new[] {'a', 'c', 'd'},

                new[] {'a', 'd', 'a'},
                new[] {'a', 'd', 'b'},
                new[] {'a', 'd', 'c'},
                new[] {'a', 'd', 'd'},

                new[] {'b', 'a', 'a'},
                new[] {'b', 'a', 'b'},
                new[] {'b', 'a', 'c'},
                new[] {'b', 'a', 'd'},

                new[] {'b', 'b', 'a'},
                new[] {'b', 'b', 'b'},
                new[] {'b', 'b', 'c'},
                new[] {'b', 'b', 'd'},

                new[] {'b', 'c', 'a'},
                new[] {'b', 'c', 'b'},
                new[] {'b', 'c', 'c'},
                new[] {'b', 'c', 'd'},

                new[] {'b', 'd', 'a'},
                new[] {'b', 'd', 'b'},
                new[] {'b', 'd', 'c'},
                new[] {'b', 'd', 'd'},

                new[] {'c', 'a', 'a'},
                new[] {'c', 'a', 'b'},
                new[] {'c', 'a', 'c'},
                new[] {'c', 'a', 'd'},

                new[] {'c', 'b', 'a'},
            };

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Take(37), Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Count(), Is.EqualTo((int) Math.Pow(4, 3))); // 64
        }

        [Test]
        public void GenerateAlphabetPermutations_FromInclusive2RangeSize2_ShouldReturn32Elements()
        {
            // ReSharper disable once StringLiteralTypo
            var enumerable = Permutations.GenerateAlphabetPermutations(3, 2, 2, "abcd");

            char[][] expected =
            {
                new[] {'c', 'a', 'a'},
                new[] {'c', 'a', 'b'},
                new[] {'c', 'a', 'c'},
                new[] {'c', 'a', 'd'},

                new[] {'c', 'b', 'a'},
                new[] {'c', 'b', 'b'},
                new[] {'c', 'b', 'c'},
                new[] {'c', 'b', 'd'},

                new[] {'c', 'c', 'a'},
                new[] {'c', 'c', 'b'},
                new[] {'c', 'c', 'c'},
                new[] {'c', 'c', 'd'},

                new[] {'c', 'd', 'a'},
                new[] {'c', 'd', 'b'},
                new[] {'c', 'd', 'c'},
                new[] {'c', 'd', 'd'},

                new[] {'d', 'a', 'a'},
                new[] {'d', 'a', 'b'},
                new[] {'d', 'a', 'c'},
                new[] {'d', 'a', 'd'},

                new[] {'d', 'b', 'a'},
                new[] {'d', 'b', 'b'},
                new[] {'d', 'b', 'c'},
                new[] {'d', 'b', 'd'},

                new[] {'d', 'c', 'a'},
                new[] {'d', 'c', 'b'},
                new[] {'d', 'c', 'c'},
                new[] {'d', 'c', 'd'},

                new[] {'d', 'd', 'a'},
                new[] {'d', 'd', 'b'},
                new[] {'d', 'd', 'c'},
                new[] {'d', 'd', 'd'},
            };

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable, Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Count(), Is.EqualTo(32));
        }

        [Test]
        public void GenerateAlphabetPermutations_FromInclusive2RangeSize1_ShouldReturn16Elements()
        {
            // ReSharper disable once StringLiteralTypo
            var enumerable = Permutations.GenerateAlphabetPermutations(3, 2, 1, "abcd");

            char[][] expected =
            {
                new[] {'c', 'a', 'a'},
                new[] {'c', 'a', 'b'},
                new[] {'c', 'a', 'c'},
                new[] {'c', 'a', 'd'},

                new[] {'c', 'b', 'a'},
                new[] {'c', 'b', 'b'},
                new[] {'c', 'b', 'c'},
                new[] {'c', 'b', 'd'},

                new[] {'c', 'c', 'a'},
                new[] {'c', 'c', 'b'},
                new[] {'c', 'c', 'c'},
                new[] {'c', 'c', 'd'},

                new[] {'c', 'd', 'a'},
                new[] {'c', 'd', 'b'},
                new[] {'c', 'd', 'c'},
                new[] {'c', 'd', 'd'},
            };

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable, Is.EquivalentTo(expected));

            // ReSharper disable once PossibleMultipleEnumeration
            Assert.That(enumerable.Count(), Is.EqualTo(16));
        }

        [Test]
        public void GenerateAlphabetPermutations_FromInclusive0RangeSize1_ShouldReturn6Elements()
        {
            // ReSharper disable once StringLiteralTypo
            var enumerable = Permutations.GenerateAlphabetPermutations(2, 0, 1, "abcdef");

            char[][] expected =
            {
                new[] {'a', 'a'},
                new[] {'a', 'b'},
                new[] {'a', 'c'},
                new[] {'a', 'd'},
                new[] {'a', 'e'},
                new[] {'a', 'f'},
            };

            Assert.That(enumerable, Is.EquivalentTo(expected));
        }

        [Test]
        public void GenerateAlphabetPermutations_FromInclusive1RangeSize1_ShouldReturn6Elements()
        {
            // ReSharper disable once StringLiteralTypo
            var enumerable = Permutations.GenerateAlphabetPermutations(2, 1, 1, "abcdef");

            char[][] expected =
            {
                new[] {'b', 'a'},
                new[] {'b', 'b'},
                new[] {'b', 'c'},
                new[] {'b', 'd'},
                new[] {'b', 'e'},
                new[] {'b', 'f'},
            };

            Assert.That(enumerable, Is.EquivalentTo(expected));
        }

        [Test]
        public void GenerateAlphabetPermutations_FromInclusive1RangeSize0_ShouldReturn6Elements()
        {
            // ReSharper disable once StringLiteralTypo
            var enumerable = Permutations.GenerateAlphabetPermutations(2, 1, 0, "abcdef");

            var expected = Array.Empty<char[]>();

            Assert.That(enumerable, Is.EquivalentTo(expected));
        }
    }
}