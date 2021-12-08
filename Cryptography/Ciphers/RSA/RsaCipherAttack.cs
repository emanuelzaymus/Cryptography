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
        public BigInteger PrimeP { get; private set; }

        /// <summary> q </summary>
        public BigInteger PrimeQ { get; private set; }

        /// <summary> Euler Phi Function </summary>
        public BigInteger Phi { get; private set; }

        /// <summary> d </summary>
        public BigInteger PrivateKey { get; private set; }

        /// <param name="publicKey"> e </param>
        /// <param name="module"> n </param>
        public RsaCipherAttack(BigInteger publicKey, BigInteger module)
        {
            _publicKey = publicKey;
            _module = module;
        }

        public void CrackPrivateKeyParallel() => CrackPrivateKeyParallel(2);

        public void CrackPrivateKeyParallel(BigInteger startFactorizationWith)
        {
            // I suppose that _module has 2 prime divisors, so I can find any of them using Divisors.FindAnyDivisorParallel method.
            // I do not check whether it's like that.
            var primeP = Divisors.FindAnyDivisorParallel(_module, startFactorizationWith);

            if (!primeP.HasValue)
            {
                throw new Exception("Module is a prime number! Does not have any prime factors.");
            }

            PrimeP = primeP.Value;
            PrimeQ = _module / PrimeP; // Should be a prime (because _modulo should have exactly 2 divisors). 

            Phi = Utils.CalculateEulerPhiFunction(PrimeP, PrimeQ);
            var privateKey = ZClass.InverseByEea(_publicKey, Phi);

            if (!privateKey.HasValue)
            {
                throw new Exception("Public key does not have any inverse element.");
            }

            PrivateKey = privateKey.Value;
        }

        public BigInteger Attack(BigInteger encryptedMessage)
        {
            if (encryptedMessage >= _module)
            {
                throw new ArgumentOutOfRangeException(nameof(encryptedMessage),
                    "Message must be lower than module of the cipher.");
            }

            return BigInteger.ModPow(encryptedMessage, PrivateKey, _module);
        }
    }
}