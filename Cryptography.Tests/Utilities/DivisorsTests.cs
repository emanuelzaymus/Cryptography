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
    }
}