using System.Diagnostics.CodeAnalysis;
using System.IO;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Utilities
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public static class Texts
    {
        private static string EnTelegraph => Read("Resources/TextsToAnalyse/en_teleg.txt");
        private static string SkTelegraph => Read("Resources/TextsToAnalyse/sk_teleg.txt");
        private static string SkWikipedia => Read("Resources/TextsToAnalyse/sk_wikipedia.txt");
        private static string Text1 => Read("Resources/Texts/text1.txt");
        private static string Text2 => Read("Resources/Texts/text2.txt");
        public static string Assignment1Text1 => Read("Resources/Assignment1/text1_enc.txt");
        public static string Assignment1Text2 => Read("Resources/Assignment1/text2_enc.txt");
        public static string Assignment1Text3 => Read("Resources/Assignment1/text3_enc.txt");
        public static string Assignment1Text4 => Read("Resources/Assignment1/text4_enc.txt");
        public static string StreamCipherMessage => Read("Resources/Texts/StreamCiphers/sprava_enc.txt");
        public static string Assignment3Text1 => Read(Paths.In.Assignment3.Text1);
        public static string Assignment3Text2 => Read(Paths.In.Assignment3.Text2);
        public static string Assignment3Text3 => Read(Paths.In.Assignment3.Text3);
        public static string Assignment3Text4 => Read(Paths.In.Assignment3.Text4);

        public static string GetEnTelegraph(TextNormalizer normalizer = null) => Normalize(EnTelegraph, normalizer);

        public static string GetSkTelegraph(TextNormalizer normalizer = null) => Normalize(SkTelegraph, normalizer);

        public static string GetSkWikipedia(TextNormalizer normalizer = null) => Normalize(SkWikipedia, normalizer);

        public static string GetText1(TextNormalizer normalizer = null) => Normalize(Text1, normalizer);

        public static string GetText2(TextNormalizer normalizer = null) => Normalize(Text2, normalizer);

        private static string Read(string path) => File.ReadAllText(path);

        private static string Normalize(string text, ITextNormalizer normalizer)
        {
            return normalizer is not null
                ? normalizer.Normalize(text)
                : text;
        }
    }
}