using System.Numerics;
using Cryptography.Utilities;
using NUnit.Framework;

namespace Cryptography.Tests.Utilities
{
    [TestFixture]
    public class ZClassTests
    {
        [Test]
        public void InverseByEea_75735792And65537_ShouldReturn31734449()
        {
            ZClass z = new(75735792);

            var inverseElement = z.InverseByEea(65537);

            Assert.That(inverseElement, Is.EqualTo(31734449));
        }

        [Test]
        public void InverseByEea_75735792AndNotPrimeValue_ShouldReturnNull()
        {
            ZClass z = new(75735792);

            var inverseElement = z.InverseByEea(65536);

            Assert.That(inverseElement.HasValue, Is.False);
        }

        [Test]
        public void InverseByEea_Task1_ShouldReturnCorrectValue()
        {
            // 13168773228 = (101279 - 1) * (130027 - 1)
            var inverseElement = ZClass.InverseByEea(65537, 13168773228);

            Assert.That(inverseElement, Is.EqualTo(new BigInteger(72739001)));
        }

        [Test]
        public void InverseByEea_Task2_ShouldReturnCorrectValue()
        {
            // 1690428403441440 = (35352181 - 1) * (47816809 - 1)
            var inverseElement = ZClass.InverseByEea(65537, 1690428403441440);

            Assert.That(inverseElement, Is.EqualTo(new BigInteger(1308297747522113)));
        }

        [Test]
        public void InverseByEea_Task3_ShouldReturnCorrectValue()
        {
            // 56341958066486836800 = (6940440583 - 1) * (8117922401 - 1)
            var inverseElement = ZClass.InverseByEea(65537, BigInteger.Parse("56341958066486836800"));

            Assert.That(inverseElement, Is.EqualTo(new BigInteger(10931906232715055873)));
        }
    }
}