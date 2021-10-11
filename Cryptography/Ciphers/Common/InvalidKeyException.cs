using System;

namespace Cryptography.Ciphers.Common
{
    public class InvalidKeyException : ArgumentException
    {
        public InvalidKeyException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}