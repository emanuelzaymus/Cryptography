using System.Numerics;
using Cryptography.Ciphers.RSA;
using NUnit.Framework;

namespace Cryptography.Tests.Ciphers.RSA
{
    [TestFixture]
    public class RsaCipherAttackTests
    {
        [Test]
        public void Attack_Task1_ShouldReturnCorrectMessage()
        {
            RsaCipherAttack attack = new(65537, 13169004533);

            attack.CrackPrivateKey();
            var decryptedMessage = attack.Attack(6029832903);

            Assert.That(decryptedMessage, Is.EqualTo(new BigInteger(1234567890)));
        }

        [Test]
        public void Attack_Task2_ShouldReturnCorrectMessage()
        {
            RsaCipherAttack attack = new(65537, 1690428486610429);

            attack.CrackPrivateKey();
            var decryptedMessage = attack.Attack(22496913456008);

            Assert.That(decryptedMessage, Is.EqualTo(new BigInteger(1234567890)));
        }

        [Test]
        public void Attack_Task3_ShouldReturnCorrectMessage()
        {
            RsaCipherAttack attack = new(65537, BigInteger.Parse("56341958081545199783"));

            attack.CrackPrivateKey(6_900_000_000);
            var decryptedMessage = attack.Attack(17014716723435111315);

            Assert.That(decryptedMessage, Is.EqualTo(new BigInteger(1234567890)));
        }
    }
}