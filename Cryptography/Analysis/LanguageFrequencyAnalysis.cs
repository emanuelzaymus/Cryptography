using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Alphabet;

namespace Cryptography.Analysis
{
    public static class LanguageFrequencyAnalysis
    {
        public static List<LetterProbability> GetProbabilitiesOfLetters(string text,
            string validLetters)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            Alphabets.CheckAlphabet(validLetters);

            var lettersCounts = CalculateLettersCounts(text, validLetters);

            double sum = lettersCounts.Values.Sum();

            if (sum == 0)
            {
                throw new Exception("No letters in the text.");
            }

            // Calculate letter probabilities for all letters
            return lettersCounts.Select(pair => new LetterProbability(pair.Key, pair.Value / sum)).ToList();
        }

        internal static Dictionary<char, double> CalculateLettersCounts(string text, string validLetters)
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
    }
}