using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptography.Utilities
{
    public static class Permutations
    {
        /// <summary>
        /// Creates permutation series. Every time returns only the same instance of <c>List</c> object!
        /// </summary>
        public static IEnumerable<List<int>> GeneratePermutationSeries(int seriesLength, int useNumberCount)
        {
            var oneSeries = Enumerable.Repeat(0, seriesLength).ToList();

            yield return oneSeries;

            for (int i = 0; i < Math.Pow(useNumberCount, seriesLength) - 1; i++)
            {
                for (int j = 0; j < seriesLength; j++)
                {
                    if (oneSeries[j] < useNumberCount - 1)
                    {
                        oneSeries[j]++;
                        break;
                    }

                    oneSeries[j] = 0;
                }

                yield return oneSeries;
            }
        }


        /// <summary>
        /// Every time returns the same instance of the array.
        /// </summary>
        public static IEnumerable<char[]> GenerateAlphabetPermutations(int wordLength, int fromInclusive, int rangeSize,
            string alphabet)
        {
            if (rangeSize == 0)
            {
                yield break;
            }

            var charArray = Enumerable.Repeat(alphabet[0], wordLength).ToArray(); // All = first elements from alphabet
            charArray[0] = alphabet[fromInclusive]; // First will be fromInclusive 

            yield return charArray;

            // Alphabet indices to Alphabet
            var wordIndices = new int[wordLength]; // All are zeros
            wordIndices[0] = fromInclusive; // First is fromInclusive

            // Subtracting 1 element because I returned first element above.
            int upToElementNumber = fromInclusive + ((int) Math.Pow(alphabet.Length, wordLength - 1) * rangeSize - 1);
            for (int i = fromInclusive; i < upToElementNumber; i++)
            {
                for (int j = wordLength - 1; j >= 0; j--)
                {
                    if (wordIndices[j] < alphabet.Length - 1)
                    {
                        wordIndices[j]++;
                        charArray[j] = alphabet[wordIndices[j]];
                        break;
                    }

                    wordIndices[j] = 0;
                    charArray[j] = alphabet[wordIndices[j]];
                }

                yield return charArray;
            }
        }
    }
}