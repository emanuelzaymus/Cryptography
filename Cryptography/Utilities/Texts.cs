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
        private static string Text1 => File.ReadAllText("Resources/Texts/text1.txt");
        private static string Text2 => File.ReadAllText("Resources/Texts/text2.txt");


        public static string GetEnTelegraph(ITextNormalizer normalizer = null) => Normalize(EnTelegraph, normalizer);

        public static string GetSkTelegraph(ITextNormalizer normalizer = null) => Normalize(SkTelegraph, normalizer);

        public static string GetText1(ITextNormalizer normalizer = null) => Normalize(Text1, normalizer);

        public static string GetText2(ITextNormalizer normalizer = null) => Normalize(Text2, normalizer);

        private static string Normalize(string text, ITextNormalizer normalizer)
        {
            return normalizer is not null
                ? normalizer.Normalize(text)
                : text;
        }
    }
}