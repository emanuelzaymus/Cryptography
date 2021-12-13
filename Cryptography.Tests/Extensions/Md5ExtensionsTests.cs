using System.Security.Cryptography;
using System.Text;
using Cryptography.Extensions;
using NUnit.Framework;

namespace Cryptography.Tests.Extensions
{
    [TestFixture]
    public class Md5ExtensionsTests
    {
        [Test]
        public void ComputeHash_WordLength7_ShouldHashProperly()
        {
            var md5 = MD5.Create();

            const string word = "7PWas22";
            const string salt = "pols58WE";
            var hashBytes = new byte[16];

            md5.ComputeHash(word.ToCharArray(), Encoding.UTF8.GetBytes(salt), hashBytes);

            var expected = MD5.HashData(Encoding.UTF8.GetBytes(word + salt));
            Assert.That(hashBytes, Is.EquivalentTo(expected));
        }

        [Test]
        public void ComputeHash_WordLength3_ShouldHashProperly()
        {
            var md5 = MD5.Create();

            const string word = "aaa";
            const string salt = "OkPa6811";
            var hashBytes = new byte[16];

            md5.ComputeHash(word.ToCharArray(), Encoding.UTF8.GetBytes(salt), hashBytes);

            var expected = MD5.HashData(Encoding.UTF8.GetBytes(word + salt));
            Assert.That(hashBytes, Is.EquivalentTo(expected));
        }

        [Test]
        public void ComputeHash_WordLength15_ShouldHashProperly()
        {
            var md5 = MD5.Create();

            const string word = "15asdE5UN9dP3Ql";
            const string salt = "9sP3rLm";
            var hashBytes = new byte[16];

            md5.ComputeHash(word.ToCharArray(), Encoding.UTF8.GetBytes(salt), hashBytes);

            var expected = MD5.HashData(Encoding.UTF8.GetBytes(word + salt));
            Assert.That(hashBytes, Is.EquivalentTo(expected));
        }

        [Test]
        public void ComputeHash_TooShortHashByteArray_ShouldNotHash()
        {
            var md5 = MD5.Create();

            const string word = "15asdE5UN9dP3Ql";
            const string salt = "9sP3rLm";
            var hashBytes = new byte[15];

            md5.ComputeHash(word.ToCharArray(), Encoding.UTF8.GetBytes(salt), hashBytes);

            var expected = MD5.HashData(Encoding.UTF8.GetBytes(word + salt));
            Assert.That(hashBytes, Is.Not.EquivalentTo(expected));
        }

        [Test]
        public void ComputeHash_TooLongHashByteArray_ShouldNotHash()
        {
            var md5 = MD5.Create();

            const string word = "15asdE5UN9dP3Ql";
            const string salt = "9sP3rLm";
            var hashBytes = new byte[15];

            md5.ComputeHash(word.ToCharArray(), Encoding.UTF8.GetBytes(salt), hashBytes);

            var expected = MD5.HashData(Encoding.UTF8.GetBytes(word + salt));
            Assert.That(hashBytes, Is.Not.EquivalentTo(expected));
        }
    }
}