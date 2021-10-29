using System.Diagnostics.CodeAnalysis;
using System.IO;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Utilities
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public static class Texts
    {
        private static string EnTelegraph => File.ReadAllText("Resources/TextsToAnalyse/en_teleg.txt");
        private static string SkTelegraph => File.ReadAllText("Resources/TextsToAnalyse/sk_teleg.txt");
        private static string SkWikipedia => File.ReadAllText("Resources/TextsToAnalyse/sk_wikipedia.txt");
        private static string Text1 => File.ReadAllText("Resources/Texts/text1.txt");
        private static string Text2 => File.ReadAllText("Resources/Texts/text2.txt");
        public static string Assignment1Text1 => File.ReadAllText("Resources/Assignment1/text1_enc.txt");
        public static string Assignment1Text2 => File.ReadAllText("Resources/Assignment1/text2_enc.txt");
        public static string Assignment1Text3 => File.ReadAllText("Resources/Assignment1/text3_enc.txt");
        public static string Assignment1Text4 => File.ReadAllText("Resources/Assignment1/text4_enc.txt");
        public static string StreamCipherMessage => File.ReadAllText("Resources/Texts/StreamCiphers/sprava_enc.txt");
        public static string Assignment3Text1 => File.ReadAllText("Resources/Assignment3/text1_enc.txt");
        public static string Assignment3Text2 => File.ReadAllText("Resources/Assignment3/text2_enc.txt");
        public static string Assignment3Text3 => File.ReadAllText("Resources/Assignment3/text3_enc.txt");
        public static string Assignment3Text4 => File.ReadAllText("Resources/Assignment3/text4_enc.txt");

        public static string GetEnTelegraph(TextNormalizer normalizer = null) => Normalize(EnTelegraph, normalizer);

        public static string GetSkTelegraph(TextNormalizer normalizer = null) => Normalize(SkTelegraph, normalizer);

        public static string GetText1(TextNormalizer normalizer = null) => Normalize(Text1, normalizer);

        public static string GetText2(TextNormalizer normalizer = null) => Normalize(Text2, normalizer);

        public static string GetSkWikipedia(TextNormalizer normalizer = null) => Normalize(SkWikipedia, normalizer);

        private static string Normalize(string text, ITextNormalizer normalizer)
        {
            return normalizer is not null
                ? normalizer.Normalize(text)
                : text;
        }
    }
}