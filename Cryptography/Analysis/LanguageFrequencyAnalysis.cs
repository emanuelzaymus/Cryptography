using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Alphabet;

namespace Cryptography.Analysis
{
    public static class LanguageFrequencyAnalysis
    {
        public static Dictionary<char, double> GetLettersProbabilities(string text, string validLetters)
        {
            CheckParameters(text, validLetters);

            return CalculateLettersProbabilities(text, validLetters);
        }

        private static Dictionary<char, double> CalculateLettersProbabilities(string text, string validLetters)
        {
            var lettersProbabilities = CalculateLettersCounts(text, validLetters);

            double sum = lettersProbabilities.Values.Sum();

            if (sum == 0)
            {
                throw new Exception("No letters in the text.");
            }

            // Calculate letter probabilities for all letters
            foreach (char key in lettersProbabilities.Keys)
            {
                lettersProbabilities[key] /= sum;
            }

            return lettersProbabilities;
        }

        private static Dictionary<char, double> CalculateLettersCounts(string text, string validLetters)
        {
            var lettersCounts = new Dictionary<char, double>(validLetters.Length);
            validLetters.ToList().ForEach(letter => lettersCounts.Add(letter, 0));

            // Count up all letter occurrences
            foreach (var ch in text)
            {
                if (lettersCounts.ContainsKey(ch))
                {
                    lettersCounts[ch]++;
                }
            }

            return lettersCounts;
        }

        public static double GetIndexOfCoincidence(string text, string validLetters)
        {
            CheckParameters(text, validLetters);

            var lettersCounts = CalculateLettersCounts(text, validLetters);

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

        private static void CheckParameters(string text, string validLetters)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            Alphabets.CheckAlphabet(validLetters);
        }
    }
}