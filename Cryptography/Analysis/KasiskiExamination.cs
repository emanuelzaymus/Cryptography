using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptography.Analysis
{
    public static class KasiskiExamination
    {
        public static List<(int Length, int DivisorsCount)> GetPasswordLengthEstimations(string text,
            int minPasswordLength, int maxPasswordLength)
        {
            var trinityDistances = GetTrinityDistances(text);

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

        public static List<(string Trinity, int Distance)> GetTrinityDistances(string text)
        {
            return GetTupleDistances(text, 3);
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
                    int compare = string.Compare(text, i, text, j, tupleLength);

                    if (compare == 0)
                    {
                        var tuple = text.Substring(i, tupleLength);

                        distances.Add((tuple, j - i));
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