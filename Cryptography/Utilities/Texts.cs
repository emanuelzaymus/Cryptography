using System.IO;
using Cryptography.Analysis.TextNormalization;

namespace Cryptography.Utilities
{
    public static class Texts
    {
        private static string EnTelegraph => Read(Paths.In.TextsToAnalyse.EnTelegraph);
        private static string SkTelegraph => Read(Paths.In.TextsToAnalyse.SkTelegraph);
        private static string SkWikipedia => Read(Paths.In.TextsToAnalyse.SkWikipedia);
        private static string Text1 => Read(Paths.In.Texts.Text1);
        private static string Text2 => Read(Paths.In.Texts.Text2);
        public static string Assignment1Text1 => Read(Paths.In.Assignment1.Text1);
        public static string Assignment1Text2 => Read(Paths.In.Assignment1.Text2);
        public static string Assignment1Text3 => Read(Paths.In.Assignment1.Text3);
        public static string Assignment1Text4 => Read(Paths.In.Assignment1.Text4);
        public static string StreamCipherMessage => Read(Paths.In.Texts.StreamCiphers.Message);
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