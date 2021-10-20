using System;
using System.Linq;
using Cryptography.Alphabet;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public abstract class AffineCipherAttack
    {
        protected readonly string Alphabet;

        private readonly int[] _divisors;

        protected AffineCipherAttack(string alphabet)
        {
            Alphabets.CheckAlphabet(alphabet);

            Alphabet = alphabet;
            _divisors = Utils.GetDivisorsWithout1(alphabet.Length);
        }

        protected bool IsDivisibleByAlphabetDivisors(int i)
        {
            return _divisors.Any(d => i % d == 0);
        }

        protected string TryDecrypt(string encryptedText, int decryptKey1, int decryptKey2)
        {
            return encryptedText.Transform((ch, _) =>
            {
                int alphabetCharIndex = Alphabet.GetCharIndex(ch);
                int newCharIndex =
                    Utils.PositiveModulo(alphabetCharIndex * decryptKey1 + decryptKey2, Alphabet.Length);
                return Alphabet[newCharIndex];
            });
        }

        protected void PrintResult(int? decryptKey1, int? decryptKey2, string decryptedText)
        {
            Console.WriteLine("{0,3} {1,3}  {2}", decryptKey1, decryptKey2, decryptedText);
        }
    }
}