using System.Collections.Generic;
using Cryptography.Extensions;

namespace Cryptography.Analysis.TextNormalization
{
    public class SlovakTextNormalizer : TextNormalizer, ITextNormalizer
    {
        private static readonly Dictionary<char, char> Letters = new()
        {
            {'á', 'a'}, {'ä', 'a'}, {'č', 'c'}, {'ď', 'd'}, {'é', 'e'}, {'í', 'i'},
            {'ĺ', 'l'}, {'ľ', 'l'}, {'ň', 'n'}, {'ó', 'o'}, {'ô', 'o'}, {'ŕ', 'r'},
            {'š', 's'}, {'ť', 't'}, {'ú', 'u'}, {'ý', 'y'}, {'ž', 'z'}, {'ě', 'e'},
            {'ř', 'r'}, {'ů', 'u'}, {'ö', 'o'}, {'ü', 'u'},
            {'Á', 'A'}, {'Ä', 'A'}, {'Č', 'C'}, {'Ď', 'D'}, {'É', 'E'}, {'Í', 'I'},
            {'Ĺ', 'L'}, {'Ľ', 'L'}, {'Ň', 'N'}, {'Ó', 'O'}, {'Ô', 'O'}, {'Ŕ', 'R'},
            {'Š', 'S'}, {'Ť', 'T'}, {'Ú', 'U'}, {'Ý', 'Y'}, {'Ž', 'Z'}, {'Ě', 'E'},
            {'Ř', 'R'}, {'Ů', 'U'}, {'Ö', 'O'}, {'Ü', 'U'}
        };

        public SlovakTextNormalizer(Casing casing = null, string onlyValidCharacters = null)
            : base(casing, onlyValidCharacters)
        {
        }

        public new string Normalize(string text)
        {
            var transformed = text.Transform(Normalization);

            return base.Normalize(transformed);
        }

        private char Normalization(char ch, int _)
        {
            return Letters.TryGetValue(ch, out char foundValue)
                ? foundValue
                : ch;
        }
    }
}