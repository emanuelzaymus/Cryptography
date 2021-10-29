using Cryptography.Alphabet;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Ciphers.Stream;
using Cryptography.RandomNumberGenerators;
using Cryptography.Utilities;

namespace Cryptography.ConsoleApp
{
    internal static class Assignment3Program
    {
        internal static void RunText1() => Run(Texts.Assignment3Text1, 0.0692); // used seed = 77777

        internal static void RunText2() => Run(Texts.Assignment3Text2, 0.0706); // used seed = 78901

        internal static void RunText3() => Run(Texts.Assignment3Text3, 0.07); // used seed = 89012

        internal static void RunText4() => Run(Texts.Assignment3Text4, 0.06539); // used seed = 98765

        private static void Run(string text, double indexOfCoincidenceThreshold)
        {
            LinearCongruentialRng rng = new(84589, 45989, 217728);
            StreamCipherBruteForceAttack attack = new(Alphabets.ALPHABET, rng);

            var normalizer = new TextNormalizer(Casing.UpperCase, Alphabets.ALPHABET);
            var normalized = normalizer.Normalize(text);

            attack.PrintAttack(normalized, text, indexOfCoincidenceThreshold);
        }
    }
}