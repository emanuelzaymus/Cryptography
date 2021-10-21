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
            var text = ReadText(analyseFile);

            return GetLettersProbabilities(text, validLetters, normalizer);
        }

        public static Dictionary<char, double> GetLettersProbabilities(string text, string validLetters,
            ITextNormalizer normalizer = null)
        {
            text = NormalizeText(text, validLetters, normalizer);

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

        public static double GetIndexOfCoincidence(FileInfo analyseFile, string validLetters,
            ITextNormalizer normalizer = null)
        {
            var text = ReadText(analyseFile);

            return GetIndexOfCoincidence(text, validLetters, normalizer);
        }

        public static double GetIndexOfCoincidence(string text, string validLetters, ITextNormalizer normalizer)
        {
            text = NormalizeText(text, validLetters, normalizer);

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

        private static string ReadText(FileInfo analyseFile)
        {
            if (analyseFile is null || !analyseFile.Exists)
            {
                throw new ArgumentException("Analyse file is null or does not exist.", nameof(analyseFile));
            }

            return File.ReadAllText(analyseFile.FullName);
        }

        private static string NormalizeText(string text, string validLetters, ITextNormalizer normalizer)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));

            Alphabets.CheckAlphabet(validLetters);

            if (normalizer is not null)
            {
                return normalizer.Normalize(text);
            }

            return text;
        }
    }
}