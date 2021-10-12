﻿using System;
using System.Linq;
using Cryptography.Alphabet;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    /// <summary>
    /// Ciphertext only brute force attack.
    /// </summary>
    public class AffineCipherBruteForceAttack
    {
        private readonly string _alphabet;

        private readonly int[] _divisors;

        public AffineCipherBruteForceAttack(string alphabet)
        {
            Alphabets.CheckAlphabet(alphabet);
            
            _alphabet = alphabet;
            _divisors = Utils.GetDivisorsWithout1(alphabet.Length);
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? decryptKey1, out int? decryptKey2)
        {
            return Attack(encryptedText, attackChecker, out decryptedText, out decryptKey1, out decryptKey2, false);
        }

        public void PrintAttack(string encryptedText)
        {
            Attack(encryptedText, null, out _, out _, out _, true);
        }

        private bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? decryptKey1, out int? decryptKey2, bool print)
        {
            for (decryptKey1 = 0; decryptKey1 < _alphabet.Length; decryptKey1++)
            {
                if (IsDivisibleByAlphabetDivisors(decryptKey1.Value))
                {
                    continue;
                }

                for (decryptKey2 = 0; decryptKey2 < _alphabet.Length; decryptKey2++)
                {
                    decryptedText = TryDecrypt(encryptedText, decryptKey1.Value, decryptKey2.Value);

                    if (print)
                    {
                        Console.WriteLine("{0,3} {1,3}  {2}", decryptKey1, decryptKey2, decryptedText);
                    }

                    if (attackChecker is not null && attackChecker.IsDecryptedCorrectly(decryptedText))
                    {
                        return true;
                    }
                }
            }

            decryptedText = null;
            decryptKey1 = null;
            decryptKey2 = null;

            return false;
        }

        private bool IsDivisibleByAlphabetDivisors(int i)
        {
            return _divisors.Any(d => i % d == 0);
        }

        private string TryDecrypt(string encryptedText, int decryptKey1, int decryptKey2)
        {
            return encryptedText.Transform(ch =>
            {
                int charIndex = _alphabet.GetCharIndex(ch);
                int newCharIndex = Utils.PositiveModulo(charIndex * decryptKey1 + decryptKey2, _alphabet.Length);
                return _alphabet[newCharIndex];
            });
        }
    }
}