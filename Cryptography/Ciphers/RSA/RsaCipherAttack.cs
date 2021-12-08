using System;
using System.Numerics;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.RSA
{
    public class RsaCipherAttack
    {
        /// <summary> e </summary>
        private readonly BigInteger _publicKey;

        /// <summary> n </summary>
        private readonly BigInteger _module;

        /// <summary> p </summary>
        private BigInteger _primeP;

        /// <summary> q </summary>
        private BigInteger _primeQ;

        /// <summary> Euler Phi Function </summary>
        private BigInteger _phi;

        /// <summary> d </summary>
        private BigInteger _privateKey;

        /// <param name="publicKey"> e </param>
        /// <param name="module"> n </param>
        public RsaCipherAttack(BigInteger publicKey, BigInteger module)
        {
            _publicKey = publicKey;
            _module = module;
        }

        public void CrackPrivateKey() => CrackPrivateKey(2);

        public void CrackPrivateKey(BigInteger startFactorizationWith)
        {
            var primeP = Primes.FindFirstPrimeFactor(_module, startFactorizationWith);

            if (!primeP.HasValue)
            {
                throw new Exception("Module is a prime number! Does not have any prime factors.");
            }

            _primeP = primeP.Value;
            _primeQ = _module / _primeP; // Should be a prime (because _modulo should have exactly 2 divisors). 

            _phi = Utils.CalculateEulerPhiFunction(_primeP, _primeQ);
            var privateKey = ZClass.InverseByEea(_publicKey, _phi);

            if (!privateKey.HasValue)
            {
                throw new Exception("Public key does not have any inverse element.");
            }

            _privateKey = privateKey.Value;
        }

        public BigInteger Attack(BigInteger encryptedMessage)
        {
            if (encryptedMessage >= _module)
            {
                throw new ArgumentOutOfRangeException(nameof(encryptedMessage),
                    "Message must be lower than module of the cipher.");
            }

            return BigInteger.ModPow(encryptedMessage, _privateKey, _module);
        }
    }
}