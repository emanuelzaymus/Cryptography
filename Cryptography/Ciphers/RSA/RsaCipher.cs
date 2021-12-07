using System;
using System.Numerics;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.RSA
{
    public class RsaCipher
    {
        private readonly BigInteger _module;
        private readonly BigInteger _publicKey;
        private readonly BigInteger _privateKey;

        public RsaCipher(BigInteger primeP, BigInteger primeQ, BigInteger publicKey, BigInteger privateKey)
        {
            // if (!Primes.IsPrime(primeP))
            //     throw new ArgumentException("P is not a prime.", nameof(primeP));
            //
            // if (!Primes.IsPrime(primeQ))
            //     throw new ArgumentException("Q is not a prime.", nameof(primeQ));

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

            // return Utils.Power(plainMessage, _publicKey, _module);
            // return (decimal) Math.Pow(plainMessage, _publicKey) % _module;
        }

        public BigInteger Decrypt(BigInteger encryptedMessage)
        {
            CheckMessage(encryptedMessage);

            return BigInteger.ModPow(encryptedMessage, _privateKey, _module);

            // return Utils.Power(encryptedMessage, _privateKey, _module);
            // return (decimal) Math.Pow(encryptedMessage, _privateKey) % _module;
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