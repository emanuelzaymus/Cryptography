using System;

namespace Cryptography.Affine
{
    // TODO: Put into Common directory
    public class InvalidKeyException : ArgumentException
    {
        public InvalidKeyException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}