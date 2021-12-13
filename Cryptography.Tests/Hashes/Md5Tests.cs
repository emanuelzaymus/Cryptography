using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cryptography.Hashes;
using NUnit.Framework;

namespace Cryptography.Tests.Hashes
{
    [TestFixture]
    public class Md5Tests
    {
        [Test]
        public void ComputeHash()
        {
            var md5 = new Md5(7 + 8);

            const string word = "7PWas22";
            const string salt = "pols58WE";
            var hashBytes = new byte[16];
            md5.ComputeHash(word.ToCharArray(), Encoding.UTF8.GetBytes(salt), hashBytes);

            var expected = MD5.HashData(Encoding.UTF8.GetBytes(word + salt));
            Assert.That(hashBytes, Is.EquivalentTo(expected));
        }

        // TODO: remove
        [Test]
        public void TTT()
        {
            const string word = "7PWas22";
            const string salt = "pols58WE";
            var bytes = Encoding.UTF8.GetBytes(word + salt);

            var md5 = MD5.Create();

            var computeHash = md5.ComputeHash(bytes);

            var res = new byte[16];
            // md5.TransformBlock(bytes, 0, bytes.Length, res, 0);
            md5.TryComputeHash(bytes, res, out _);

            Assert.That(computeHash, Is.EquivalentTo(res));
        }
    }
}