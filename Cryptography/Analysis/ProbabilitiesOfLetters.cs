using System.Collections.Generic;
using Cryptography.Alphabet;
using Cryptography.Analysis.TextNormalization;
using Cryptography.Utilities;

namespace Cryptography.Analysis
{
    public static class ProbabilitiesOfLetters
    {
        public static readonly List<LetterProbability> EnTelegraphWithSpace = new()
        {
            new('A', 0.0657),
            new('B', 0.0126),
            new('C', 0.0399),
            new('D', 0.0322),
            new('E', 0.0957),
            new('F', 0.0175),
            new('G', 0.0145),
            new('H', 0.0404),
            new('I', 0.0701),
            new('J', 0.0012),
            new('K', 0.0049),
            new('L', 0.0246),
            new('M', 0.0231),
            new('N', 0.0551),
            new('O', 0.0603),
            new('P', 0.0298),
            new('Q', 0.0005),
            new('R', 0.0576),
            new('S', 0.0581),
            new('T', 0.0842),
            new('U', 0.0192),
            new('V', 0.0081),
            new('W', 0.0086),
            new('X', 0.0007),
            new('Y', 0.0167),
            new('Z', 0.0005),
            new(' ', 0.1580)
        };

        public static readonly List<LetterProbability> SkTelegraphWithSpace = new()
        {
            new('A', 0.0995),
            new('B', 0.0118),
            new('C', 0.0266),
            new('D', 0.0436),
            new('E', 0.0698),
            new('F', 0.0113),
            new('G', 0.0017),
            new('H', 0.0175),
            new('I', 0.0711),
            new('J', 0.0157),
            new('K', 0.0406),
            new('L', 0.0262),
            new('M', 0.0354),
            new('N', 0.0646),
            new('O', 0.0812),
            new('P', 0.0179),
            new('Q', 0.0000),
            new('R', 0.0428),
            new('S', 0.0463),
            new('T', 0.0432),
            new('U', 0.0384),
            new('V', 0.0314),
            new('W', 0.0000),
            new('X', 0.0004),
            new('Y', 0.0170),
            new('Z', 0.0175),
            new(' ', 0.1283)
        };

        public static List<LetterProbability> EnTelegraphWithoutSpace =>
            LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(Texts.GetEnTelegraph(), Alphabets.ALPHABET);

        public static List<LetterProbability> SkTelegraphWithoutSpace =>
            LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(
                Texts.GetSkTelegraph(new SlovakTextNormalizer(Casing.UpperCase)), Alphabets.ALPHABET);

        public static List<LetterProbability> SkWikipedia => LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(
            Texts.GetSkWikipedia(new SlovakTextNormalizer(Casing.UpperCase)), Alphabets.ALPHABET);
    }
}