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

            Assert.That(inverseElement.HasValue, Is.True);
            // ReSharper disable once PossibleInvalidOperationException
            Assert.That(inverseElement.Value, Is.EqualTo(31734449));
        }

        [Test]
        public void InverseByEea_75735792AndNotPrimeValue_ShouldReturnNull()
        {
            ZClass z = new(75735792);

            var inverseElement = z.InverseByEea(65536);

            Assert.That(inverseElement.HasValue, Is.False);
        }
    }
}