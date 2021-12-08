using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Utilities
{
    [TestFixture]
    public class DivisorsTests
    {
        [Test]
        public void GetDivisors()
        {
            Assert.AreEqual(new[] {1}, Divisors.GetDivisors(1));
            Assert.AreEqual(new[] {1, 2}, Divisors.GetDivisors(2));
            Assert.AreEqual(new[] {1, 3}, Divisors.GetDivisors(3));
            Assert.AreEqual(new[] {1, 2, 4}, Divisors.GetDivisors(4));
            Assert.AreEqual(new[] {1, 5}, Divisors.GetDivisors(5));
            Assert.AreEqual(new[] {1, 2, 5, 10}, Divisors.GetDivisors(10));
            Assert.AreEqual(new[] {1, 2, 3, 4, 6, 8, 12, 16, 24, 48}, Divisors.GetDivisors(48));
        }

        [Test]
        public void FindAnyDivisorParallel_Task1_ShouldReturnOneDivisor()
        {
            var anyDivisor = Divisors.FindAnyDivisorParallel(13169004533);

            Assert.That(anyDivisor, Is.EqualTo(new BigInteger(101279)));
        }

        [Test]
        public void FindAnyDivisorParallel_Task2_ShouldReturnOneDivisor()
        {
            var anyDivisor = Divisors.FindAnyDivisorParallel(1690428486610429);

            Assert.That(anyDivisor, Is.EqualTo(new BigInteger(35352181)));
        }

        [Test]
        public void FindAnyDivisorParallel_Task3_ShouldReturnOneDivisor()
        {
            var stopwatch = Stopwatch.StartNew();
            var anyDivisor = Divisors.FindAnyDivisorParallel(BigInteger.Parse("56341958081545199783"), 6_000_000_000);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            Assert.That(anyDivisor, Is.EqualTo(new BigInteger(6940440583)));
        }

        [Test]
        public void SplitRange()
        {
            BigInteger startValue = 2;
            BigInteger splitSize = 938266004;

            var bi1 = startValue + splitSize * 1;
            var bi2 = startValue + splitSize * 2;
            var bi3 = startValue + splitSize * 3;
            var bi4 = startValue + splitSize * 4;
            var bi5 = startValue + splitSize * 5;
            var bi6 = startValue + splitSize * 6;
            var bi7 = startValue + splitSize * 7;
            var bi8 = startValue + splitSize * 8;

            List<(BigInteger FromInclusive, BigInteger ToExclusive)> bounds = new()
            {
                (startValue, bi1), (bi1, bi2), (bi2, bi3), (bi3, bi4), (bi4, bi5), (bi5, bi6), (bi6, bi7), (bi7, bi8)
            };

            var splits = Divisors.SplitRange(2, 7506128035, 8);

            Assert.That(splits, Is.EquivalentTo(bounds));
        }
    }
}