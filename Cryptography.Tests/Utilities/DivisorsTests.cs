using System.Collections.Generic;
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
            var anyDivisor = Divisors.FindAnyDivisorParallel(BigInteger.Parse("56341958081545199783"), 6_500_000_000);

            Assert.That(anyDivisor, Is.EqualTo(new BigInteger(6940440583)));
        }
    }
}