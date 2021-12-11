using System.Security.Cryptography;

namespace Cryptography.Hashes
{
    public abstract class Md5Attack
    {
        protected readonly MD5 Md5 = MD5.Create();
    }
}