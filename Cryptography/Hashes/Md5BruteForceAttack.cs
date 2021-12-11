using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Alphabet;

namespace Cryptography.Hashes
{
    public class Md5BruteForceAttack : Md5Attack
    {
        private readonly string _alphabet;
        private readonly int _minLength;
        private readonly int _maxLength;

        public Md5BruteForceAttack(string alphabet, int minLength, int maxLength)
        {
            Alphabets.CheckAlphabet(alphabet);

            _alphabet = alphabet;
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public override bool TryCrackPassword(string passwordHash, string salt, out string crackedPassword)
        {
            byte[] passwordHashBytes = HashToByteArray(passwordHash);
            byte[] saltBytes = StringToByteArray(salt);

            for (int wordLength = _minLength; wordLength <= _maxLength; wordLength++)
            {
                var permutation = AlphabetPermutations(wordLength);

                foreach (var wordChars in permutation)
                {
                    byte[] hash = ComputeHash(wordChars, saltBytes);

                    if (hash.SequenceEqual(passwordHashBytes))
                    {
                        crackedPassword = new string(wordChars);
                        return true;
                    }
                }
            }

            crackedPassword = null;
            return false;
        }

        /// <summary>
        /// Every time returns the same instance of the array.
        /// </summary>
        public IEnumerable<char[]> AlphabetPermutations(int wordLength)
        {
            var charArray = Enumerable.Repeat(_alphabet[0], wordLength).ToArray();

            yield return charArray;

            // Alphabet indices to Alphabet
            var wordIndices = new int[wordLength];

            // Subtracting 1 element because I returned first element above.
            int totalNumberOfElements = (int) (Math.Pow(_alphabet.Length, wordLength) - 1);
            for (int i = 0; i < totalNumberOfElements; i++)
            {
                for (int j = wordLength - 1; j >= 0; j--)
                {
                    if (wordIndices[j] < _alphabet.Length - 1)
                    {
                        wordIndices[j]++;
                        charArray[j] = _alphabet[wordIndices[j]];
                        break;
                    }

                    wordIndices[j] = 0;
                    charArray[j] = _alphabet[wordIndices[j]];
                }

                yield return charArray;
            }
        }
    }
}