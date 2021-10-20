using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Analysis
{
    public class LanguageFrequencyAnalysis : ILettersProbabilities
    {
        private readonly Dictionary<char, double> _lettersProbabilities;

        public LanguageFrequencyAnalysis(string analyseFilePath, string validLetters, ITextNormalizer normalizer = null)
        {
            if (string.IsNullOrEmpty(validLetters))
                throw new ArgumentException("Value cannot be null or empty.", nameof(validLetters));

            var text = File.ReadAllText(analyseFilePath);

            if (normalizer is not null)
            {
                text = normalizer.Normalize(text);
            }

            _lettersProbabilities = new(validLetters.Length);
            validLetters.ToList().ForEach(letter => _lettersProbabilities.Add(letter, 0));

            Analyse(text);
        }

        private void Analyse(string text)
        {
            int sum = 0;

            // Count up all letter occurrences
            foreach (var ch in text)
            {
                if (_lettersProbabilities.ContainsKey(ch))
                {
                    _lettersProbabilities[ch]++;
                    sum++;
                }
            }

            if (sum == 0)
            {
                throw new Exception("No letters in the text.");
            }

            // Calculate letter probabilities for all letters
            foreach (char key in _lettersProbabilities.Keys)
            {
                _lettersProbabilities[key] /= sum;
            }
        }

        public Dictionary<char, double> GetLettersProbabilities() => _lettersProbabilities;
    }
}