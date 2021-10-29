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
        private static string Assignment1Text1 => File.ReadAllText("Resources/Assignment1/text1_enc.txt");
        private static string Assignment1Text2 => File.ReadAllText("Resources/Assignment1/text2_enc.txt");
        private static string Assignment1Text3 => File.ReadAllText("Resources/Assignment1/text3_enc.txt");
        private static string Assignment1Text4 => File.ReadAllText("Resources/Assignment1/text4_enc.txt");
        private static string StreamCipherMessage => File.ReadAllText("Resources/Texts/StreamCiphers/sprava_enc.txt");

        public static string GetEnTelegraph(TextNormalizer normalizer = null) => Normalize(EnTelegraph, normalizer);

        public static string GetSkTelegraph(TextNormalizer normalizer = null) => Normalize(SkTelegraph, normalizer);

        public static string GetText1(TextNormalizer normalizer = null) => Normalize(Text1, normalizer);

        public static string GetText2(TextNormalizer normalizer = null) => Normalize(Text2, normalizer);

        public static string GetSkWikipedia(TextNormalizer normalizer = null) => Normalize(SkWikipedia, normalizer);

        public static string GetAssignment1Text1(TextNormalizer normalizer = null)
            => Normalize(Assignment1Text1, normalizer);

        public static string GetAssignment1Text2(TextNormalizer normalizer = null)
            => Normalize(Assignment1Text2, normalizer);

        public static string GetAssignment1Text3(TextNormalizer normalizer = null)
            => Normalize(Assignment1Text3, normalizer);

        public static string GetAssignment1Text4(TextNormalizer normalizer = null)
            => Normalize(Assignment1Text4, normalizer);

        public static string GetStreamCipherMessage(TextNormalizer normalizer = null) =>
            Normalize(StreamCipherMessage, normalizer);

        private static string Normalize(string text, ITextNormalizer normalizer)
        {
            return normalizer is not null
                ? normalizer.Normalize(text)
                : text;
        }
    }
}