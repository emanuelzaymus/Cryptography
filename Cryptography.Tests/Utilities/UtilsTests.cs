using System.Collections.Generic;
using System.Numerics;
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
        public void SplitRange_BigIntegers()
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

            var splits = Utils.SplitRange(2, 7506128035, 8);

            Assert.That(splits, Is.EquivalentTo(bounds));
        }

        [Test]
        public void SplitRange_Integers()
        {
            List<(int FromInclusive, int ToExclusive)> bounds = new()
            {
                (0, 3), (3, 4), (7, 3), (10, 3), (13, 3), (16, 4), (20, 3), (23, 3)
            };

            var splits = Utils.SplitRange(26, 8);

            Assert.That(splits, Is.EquivalentTo(bounds));
        }
    }
}