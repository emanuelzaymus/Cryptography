using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cryptography.Alphabet;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Analysis
{
    public static class KasiskiExamination
    {
        public static List<(int Length, int DivisorsCount)> GetPasswordLengthEstimations(string text, string alphabet,
            int minPasswordLength, int maxPasswordLength)
        {
            var trinityDistances = GetTrinityDistances(text, alphabet);

            List<Estimation> estimations = new();

            for (int i = minPasswordLength; i <= maxPasswordLength; i++)
            {
                estimations.Add(new(i, 0));
            }

            foreach (var trinityDistance in trinityDistances)
            {
                foreach (var estimation in estimations)
                {
                    if (trinityDistance.Distance % estimation.Length == 0)
                    {
                        estimation.DivisorsCount++;
                    }
                }
            }

            return estimations
                .OrderByDescending(e => e.DivisorsCount)
                .ThenBy(e => e.Length)
                .Select(e => (e.Length, e.DivisorsCount))
                .ToList();
        }

        public static List<(string Trinity, int Distance)> GetTrinityDistances(FileInfo file, string alphabet,
            ITextNormalizer normalizer = null)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            var text = File.ReadAllText(file.FullName);

            return GetTrinityDistances(text, alphabet, normalizer);
        }

        public static List<(string Trinity, int Distance)> GetTrinityDistances(string text, string alphabet,
            ITextNormalizer normalizer = null)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            Alphabets.CheckAlphabet(alphabet);

            if (normalizer is not null)
            {
                text = normalizer.Normalize(text);
            }

            text = RemoveInvalidCharacters(text, alphabet);

            return GetTupleDistances(text, 3);
        }

        private static string RemoveInvalidCharacters(string text, string alphabet)
        {
            return string.Concat(text.Where(alphabet.Contains));
        }

        private static List<(string Tuple, int Distance)> GetTupleDistances(string text, int tupleLength)
        {
            if (tupleLength < 1)
                throw new ArgumentOutOfRangeException(nameof(tupleLength), "Tuple length must be positive number.");

            if (text is null || text.Length < tupleLength + 1)
                throw new ArgumentException("Text is null or is too short.", nameof(text));

            List<(string, int)> distances = new();

            for (int i = 0; i < text.Length - tupleLength; i++)
            {
                for (int j = i + 1; j < text.Length - tupleLength; j++)
                {
                    string first = text.Substring(i, tupleLength);
                    string second = text.Substring(j, tupleLength);

                    if (first == second)
                    {
                        distances.Add((first, j - i));
                    }
                }
            }

            return distances;
        }

        private record Estimation(int Length, int DivisorsCount)
        {
            public int DivisorsCount { get; set; } = DivisorsCount;
        };
    }
}