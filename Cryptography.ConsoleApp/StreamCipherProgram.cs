using Cryptography.Alphabet;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Ciphers.Stream;
using Cryptography.RandomNumberGenerators;
using Cryptography.Utilities;

namespace Cryptography.ConsoleApp
{
    internal static class StreamCipherProgram
    {
        internal static void Run()
        {
            IRng rng = new LinearCongruentialRng(8121, 28411, 134456, -1);
            StreamCipherBruteForceAttack attack = new(Alphabets.ALPHABET, rng);

            var text = Texts.GetStreamCipherMessage();
            var normalizer = new TextNormalizer(Casing.UpperCase, Alphabets.ALPHABET);
            var normalized = normalizer.Normalize(text);

            attack.PrintAttack(normalized, text);
        }
    }
}