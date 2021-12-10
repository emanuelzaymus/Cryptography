using System.Collections.Generic;
using System.Linq;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Utilities
{
    public static class SlovakFirstNames
    {
        public static List<string> GetNamesWithDiminutives()
        {
            return GetFemaleNamesWithDiminutives().Concat(GetMaleNamesWithDiminutives()).ToList();
        }

        private static IEnumerable<string> GetFemaleNamesWithDiminutives()
        {
            var slovakTextNormalizer = new SlovakTextNormalizer(Casing.LowerCase);
            var names = Texts.GetSlovakFemaleNames(slovakTextNormalizer)
                .Split('\n')
                .Select(n => n.Trim());

            foreach (var name in names)
            {
                yield return name;

                var diminutive = CreateFemaleDiminutive(name);

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

        private static IEnumerable<string> GetMaleNamesWithDiminutives()
        {
            var slovakTextNormalizer = new SlovakTextNormalizer(Casing.LowerCase);
            var names = Texts.GetSlovakMaleNames(slovakTextNormalizer)
                .Split('\n')
                .Select(n => n.Trim());

            foreach (var name in names)
            {
                yield return name;

                var diminutive = CreateMaleDiminutive(name);

                if (diminutive is not null)
                {
                    yield return diminutive;
                }
            }
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