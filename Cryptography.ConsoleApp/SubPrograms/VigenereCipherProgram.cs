using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Ciphers.Vigenere;
using Cryptography.Utilities;

namespace Cryptography.ConsoleApp.SubPrograms
{
    internal static class VigenereCipherProgram
    {
        internal static void Run()
        {
            VigenereCipherKasiskiAttack attack =
                new(Alphabets.ALPHABET, ProbabilitiesOfLetters.SkTelegraphWithoutSpace);

            var text1 = Texts.GetText1();
            var normalizer = new TextNormalizer(Casing.UpperCase, Alphabets.ALPHABET);
            var normalized = normalizer.Normalize(text1);

            attack.PrintAttack(normalized, text1, 3, 8);
        }
    }
}