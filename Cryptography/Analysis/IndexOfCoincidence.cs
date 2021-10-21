using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Alphabet;

namespace Cryptography.Analysis
{
    public static class IndexOfCoincidence
    {
        public const double Threshold = 0.049;

        public static double GetIndexOfCoincidence(string text, string validLetters)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            Alphabets.CheckAlphabet(validLetters);

            var lettersCounts = LanguageFrequencyAnalysis.CalculateLettersCounts(text, validLetters);

            return CalculateIndexOfCoincidence(lettersCounts);
        }

        private static double CalculateIndexOfCoincidence(Dictionary<char, double> lettersCounts)
        {
            double indexOfCoincidence = 0;

            double sum = lettersCounts.Values.Sum();

            foreach (double letterCount in lettersCounts.Values)
            {
                indexOfCoincidence += (letterCount / sum) * ((letterCount - 1) / (sum - 1));
            }

            return indexOfCoincidence;
        }
    }
}