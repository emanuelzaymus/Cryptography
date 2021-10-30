using System.Collections.Generic;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Ciphers.Vigenere;
using Cryptography.Utilities;

namespace Cryptography.ConsoleApp.SubPrograms
{
    internal static class Assignment1Program
    {
        internal static void RunText1()
        {
            Run(Texts.Assignment1Text1, ProbabilitiesOfLetters.SkWikipedia);
        }

        internal static void RunText2()
        {
            Run(Texts.Assignment1Text2, ProbabilitiesOfLetters.SkWikipedia);
        }

        internal static void RunText3()
        {
            Run(Texts.Assignment1Text3, ProbabilitiesOfLetters.SkWikipedia);
        }

        internal static void RunText4()
        {
            Run(Texts.Assignment1Text4, ProbabilitiesOfLetters.EnTelegraphWithoutSpace);
        }

        private static void Run(string text, List<LetterProbability> probabilitiesOfLetters)
        {
            VigenereCipherKasiskiAttack attack = new(Alphabets.ALPHABET, probabilitiesOfLetters);

            var normalizer = new TextNormalizer(Casing.UpperCase, Alphabets.ALPHABET);
            var normalized = normalizer.Normalize(text);

            attack.PrintAttack(normalized, text, 20, 29);
        }
    }
}