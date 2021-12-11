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
                var length = wordLength;
                Parallel.ForEach(alphabetSplits, new ParallelOptions {MaxDegreeOfParallelism = processorCount}, split =>
                {
                    var (fromInclusive, rangeSize) = split;
                    var permutations = Permutations.GenerateAlphabetPermutations(length, fromInclusive,
                        rangeSize, _alphabet);

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

        // TODO: Optimize
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