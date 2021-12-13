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

        /// <summary>
        /// Parallel password cracking.
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <param name="salt"></param>
        /// <param name="crackedPassword"></param>
        /// <returns></returns>
        public override bool TryCrackPassword(string passwordHash, string salt, out string crackedPassword)
        {
            byte[] passwordHashBytes = HashToByteArray(passwordHash);
            byte[] saltBytes = StringToByteArray(salt);

            var processorCount = Environment.ProcessorCount;
            var alphabetSplits = Utils.SplitRange(_alphabet.Length, processorCount);

            ConcurrentBag<string> crackedPasswordConcurrentBag = new();

            for (int wordLength = _minLength; wordLength <= _maxLength; wordLength++)
            {
                var currentWordLength = wordLength;
                Parallel.ForEach(alphabetSplits, new ParallelOptions {MaxDegreeOfParallelism = processorCount}, split =>
                {
                    var (fromInclusive, rangeSize) = split;
                    var permutations = Permutations.GenerateAlphabetPermutations(currentWordLength, fromInclusive,
                        rangeSize, _alphabet);

                    FindMatchingPasswordHash(permutations, currentWordLength, saltBytes, passwordHashBytes,
                        crackedPasswordConcurrentBag);
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

        private void FindMatchingPasswordHash(IEnumerable<char[]> alphabetPermutations, int wordLength,
            byte[] saltBytes, byte[] passwordHashBytes, ConcurrentBag<string> crackedPasswordConcurrentBag)
        {
            // var md5 = MD5.Create();
            var md5 = new Md5(wordLength + saltBytes.Length);
            var hashBytes = new byte[Md5.OutputByteArrayLength];

            int counter = 0;
            foreach (var wordChars in alphabetPermutations)
            {
                // byte[] hash = ComputeHash(wordChars, saltBytes, md5);
                md5.ComputeHash(wordChars, saltBytes, hashBytes);

                if (hashBytes.SequenceEqual(passwordHashBytes))
                {
                    crackedPasswordConcurrentBag.Add(new string(wordChars));
                }

                if (counter++ >= 1_000_000)
                {
                    if (!crackedPasswordConcurrentBag.IsEmpty)
                    {
                        return;
                    }

                    counter = 0;
                }
            }
        }
    }
}