using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Cryptography.Alphabet;
using Cryptography.Utilities;

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

            var processorCount = Environment.ProcessorCount;
            var alphabetSplits = Divisors.SplitRange(_alphabet.Length, processorCount);

            ConcurrentBag<string> crackedPasswordConcurrentBag = new();

            for (int wordLength = _minLength; wordLength <= _maxLength; wordLength++)
            {
                var length = wordLength;
                Parallel.ForEach(alphabetSplits, new ParallelOptions {MaxDegreeOfParallelism = processorCount}, split =>
                {
                    var (fromInclusive, rangeSize) = split;
                    var permutations = AlphabetPermutations(length, fromInclusive, rangeSize);

                    FindMatchingPasswordHash(permutations, saltBytes, passwordHashBytes, crackedPasswordConcurrentBag);
                });

                if (crackedPasswordConcurrentBag.Any())
                {
                    crackedPassword = crackedPasswordConcurrentBag.First();
                    return true;
                }
            }

            crackedPassword = null;
            return false;
        }

        private void FindMatchingPasswordHash(IEnumerable<char[]> alphabetPermutations, byte[] saltBytes,
            byte[] passwordHashBytes, ConcurrentBag<string> crackedPasswordConcurrentBag)
        {
            var md5 = MD5.Create();

            foreach (var wordChars in alphabetPermutations)
            {
                byte[] hash = ComputeHashOptimized(wordChars, saltBytes, md5);

                if (hash.SequenceEqual(passwordHashBytes))
                {
                    crackedPasswordConcurrentBag.Add(new string(wordChars));
                }
            }
        }

        /// <summary>
        /// Every time returns the same instance of the array.
        /// </summary>
        public IEnumerable<char[]> AlphabetPermutations(int wordLength, int fromInclusive, int rangeSize)
        {
            if (rangeSize == 0)
            {
                yield break;
            }

            var charArray = Enumerable.Repeat(_alphabet[0], wordLength).ToArray(); // All = first elements from alphabet
            charArray[0] = _alphabet[fromInclusive]; // First will be fromInclusive 

            yield return charArray;

            // Alphabet indices to Alphabet
            var wordIndices = new int[wordLength]; // All are zeros
            wordIndices[0] = fromInclusive; // First is fromInclusive

            // Subtracting 1 element because I returned first element above.
            int upToElementNumber = fromInclusive + ((int) Math.Pow(_alphabet.Length, wordLength - 1) * rangeSize - 1);
            for (int i = fromInclusive; i < upToElementNumber; i++)
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

        private byte[] ComputeHashOptimized(char[] wordChars, byte[] saltBytes, MD5 md5)
        {
            // Creates every time a new byte array.
            byte[] wordBytes = CharArrayToByteArray(wordChars);

            // Another byte array created.
            var concatenated = wordBytes.Concat(saltBytes).ToArray();

            // New byte array created again.
            return md5.ComputeHash(concatenated);
        }
    }
}