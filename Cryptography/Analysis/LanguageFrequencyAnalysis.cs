using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Alphabet;

namespace Cryptography.Analysis
{
    public static class LanguageFrequencyAnalysis
    {
        public static List<LetterProbability> GetProbabilitiesOfLetters(string text, string validLetters)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            Alphabets.CheckAlphabet(validLetters);

            var lettersCounts = CalculateLettersCounts(text, validLetters);

            double sum = lettersCounts.Sum();

            if (sum == 0)
            {
                throw new Exception("No letters in the text.");
            }

            // Calculate letter probabilities for all letters
            return validLetters.Zip(lettersCounts, (ch, count) => new LetterProbability(ch, count / sum)).ToList();
        }

        internal static int[] CalculateLettersCounts(string text, string validLetters)
        {
            var lettersCounts = new int[validLetters.Length];

            // Count up all letter occurrences
            foreach (var ch in text)
            {
                int index = validLetters.IndexOf(ch);

                if (index >= 0)
                {
                    lettersCounts[index]++;
                }
            }

            return lettersCounts;
        }
    }
}