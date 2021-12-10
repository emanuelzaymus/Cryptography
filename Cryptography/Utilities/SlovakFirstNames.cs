using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Utilities
{
    public static class SlovakFirstNames
    {
        private static readonly SlovakTextNormalizer SlovakTextNormalizer = new(Casing.LowerCase);

        public static List<string> GetNamesWithDiminutives()
        {
            return GetFemaleNamesWithDiminutives().Concat(GetMaleNamesWithDiminutives()).ToList();
        }

        private static IEnumerable<string> GetFemaleNamesWithDiminutives()
        {
            return GetNamesWithDiminutives(Texts.GetSlovakFemaleNames(SlovakTextNormalizer), CreateFemaleDiminutive);
        }

        private static IEnumerable<string> GetMaleNamesWithDiminutives()
        {
            return GetNamesWithDiminutives(Texts.GetSlovakMaleNames(SlovakTextNormalizer), CreateMaleDiminutive);
        }

        private static IEnumerable<string> GetNamesWithDiminutives(string allNamesInLines,
            Func<string, string> createDiminutive)
        {
            var names = allNamesInLines
                .Split('\n')
                .Select(n => n.Trim());

            foreach (var name in names)
            {
                yield return name;

                var diminutive = createDiminutive(name);

                if (diminutive is not null)
                {
                    yield return diminutive;
                }
            }
        }

        private static string CreateFemaleDiminutive(string femaleName)
        {
            if (femaleName.EndsWith("ka"))
            {
                return null;
            }

            // All Slovak female names terminates with 'a' => I need to remove the last character first.
            return femaleName.Remove(femaleName.Length - 1) + "ka";
        }

        private static string CreateMaleDiminutive(string maleName)
        {
            if (maleName.EndsWith("ko") || maleName.EndsWith('k') || maleName.EndsWith('a') ||
                maleName.EndsWith("ch") || maleName.EndsWith('e'))
            {
                return null;
            }

            if (maleName.EndsWith('o'))
            {
                return maleName.Remove(maleName.Length - 1) + "ko";
            }

            return maleName + "ko";
        }
    }
}