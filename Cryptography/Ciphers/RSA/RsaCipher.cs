using System;
using System.Numerics;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.RSA
{
    public class RsaCipher
    {
        /// <summary> n </summary>
        private readonly BigInteger _module;

        /// <summary> e </summary>
        private readonly BigInteger _publicKey;

        /// <summary> d </summary>
        private readonly BigInteger _privateKey;

        /// <summary>
        /// Primes P and Q are not check to be primes. 
        /// </summary>
        /// <param name="primeP"> p </param>
        /// <param name="primeQ"> q </param>
        /// <param name="publicKey"> e </param>
        /// <param name="privateKey"> d </param>
        public RsaCipher(BigInteger primeP, BigInteger primeQ, BigInteger publicKey, BigInteger privateKey)
        {
            _module = primeP * primeQ;

            if (publicKey >= _module)
                throw new ArgumentOutOfRangeException(nameof(publicKey),
                    "Public key must be lower than primeP * primeQ.");

            if (privateKey >= _module)
                throw new ArgumentOutOfRangeException(nameof(privateKey),
                    "Private key must be lower than primeP * primeQ.");

            var phi = Utils.CalculateEulerPhiFunction(primeP, primeQ);

            if ((publicKey * privateKey) % phi != 1)
            {
                throw new ArgumentException("Public and private keys need to be inverse elements.");
            }

            _publicKey = publicKey;
            _privateKey = privateKey;
        }

        public BigInteger Encrypt(BigInteger plainMessage)
        {
            CheckMessage(plainMessage);

            return BigInteger.ModPow(plainMessage, _publicKey, _module);
        }

        public BigInteger Decrypt(BigInteger encryptedMessage)
        {
            CheckMessage(encryptedMessage);

            return BigInteger.ModPow(encryptedMessage, _privateKey, _module);
        }

        private void CheckMessage(BigInteger message)
        {
            if (message >= _module)
            {
                throw new ArgumentOutOfRangeException(nameof(message),
                    "Message must be lower than module of the cipher.");
            }
        }
    }
}