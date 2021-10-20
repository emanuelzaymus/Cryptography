using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cryptography.Alphabet;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Analysis
{
    public static class LanguageFrequencyAnalysis
    {
        public static Dictionary<char, double> GetLettersProbabilities(FileInfo analyseFile, string validLetters,
            ITextNormalizer normalizer = null)
        {
            if (analyseFile is null || !analyseFile.Exists)
            {
                throw new ArgumentException("Analyse file is null or does not exist.", nameof(analyseFile));
            }

            var text = File.ReadAllText(analyseFile.FullName);

            return GetLettersProbabilities(text, validLetters, normalizer);
        }

        public static Dictionary<char, double> GetLettersProbabilities(string text, string validLetters,
            ITextNormalizer normalizer = null)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            Alphabets.CheckAlphabet(validLetters);

            if (normalizer is not null)
            {
                text = normalizer.Normalize(text);
            }

            return Analyse(text, validLetters);
        }

        private static Dictionary<char, double> Analyse(string text, string validLetters)
        {
            var lettersProbabilities = new Dictionary<char, double>(validLetters.Length);
            validLetters.ToList().ForEach(letter => lettersProbabilities.Add(letter, 0));

            int sum = 0;

            // Count up all letter occurrences
            foreach (var ch in text)
            {
                if (lettersProbabilities.ContainsKey(ch))
                {
                    lettersProbabilities[ch]++;
                    sum++;
                }
            }

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
    }
}