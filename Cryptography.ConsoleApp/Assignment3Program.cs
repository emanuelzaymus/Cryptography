using Cryptography.Alphabet;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Ciphers.Stream;
using Cryptography.RandomNumberGenerators;
using Cryptography.Utilities;

namespace Cryptography.ConsoleApp
{
    internal static class Assignment3Program
    {
        internal static void RunText1()
        {
            // Used seed = 77777
            Run(Texts.Assignment3Text1, Paths.Assignment3Text1Decrypted, 0.0692);
        }

        internal static void RunText2()
        {
            // Used seed = 78901
            Run(Texts.Assignment3Text2, Paths.Assignment3Text2Decrypted, 0.0706);
        }

        internal static void RunText3()
        {
            // Used seed = 89012
            Run(Texts.Assignment3Text3, Paths.Assignment3Text3Decrypted, 0.07);
        }

        internal static void RunText4()
        {
            // Used seed = 98765
            Run(Texts.Assignment3Text4, Paths.Assignment3Text4Decrypted, 0.06539);
        }

        private static void Run(string text, string writeToFile, double indexOfCoincidenceThreshold)
        {
            LinearCongruentialRng rng = new(84589, 45989, 217728);
            StreamCipherBruteForceAttack attack = new(Alphabets.ALPHABET, rng);

            var normalizer = new TextNormalizer(Casing.UpperCase, Alphabets.ALPHABET);
            var normalized = normalizer.Normalize(text);

            attack.PrintAttack(normalized, text, writeToFile, indexOfCoincidenceThreshold);
        }
    }
}