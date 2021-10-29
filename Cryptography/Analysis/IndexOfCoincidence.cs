using System;
using System.Linq;
using Cryptography.Alphabet;

namespace Cryptography.Analysis
{
    public static class IndexOfCoincidence
    {
        public const double Threshold = 0.049;
        public const double HigherThreshold = 0.06;

        public static double GetIndexOfCoincidence(string text, string validLetters)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            Alphabets.CheckAlphabet(validLetters);

            var lettersCounts = new int[validLetters.Length];
            LanguageFrequencyAnalysis.CalculateLettersCounts(text, validLetters, lettersCounts);

            return CalculateIndexOfCoincidence(lettersCounts);
        }

        private static double CalculateIndexOfCoincidence(int[] lettersCounts)
        {
            double indexOfCoincidence = 0;

            double sum = lettersCounts.Sum();

            foreach (double letterCount in lettersCounts)
            {
                indexOfCoincidence += (letterCount / sum) * ((letterCount - 1) / (sum - 1));
            }

            return indexOfCoincidence;
        }
    }
}