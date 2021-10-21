﻿using System.Linq;

namespace Cryptography.Analysis.TextNormalization
{
    public class TextNormalizer : ITextNormalizer
    {
        private readonly Casing _casing;
        private readonly string _onlyValidCharacters;

        /// <summary>
        /// Custom text normalization.
        /// </summary>
        /// <param name="casing">Transforms all characters to upper case or lowercase</param>
        /// <param name="onlyValidCharacters">If <paramref name="onlyValidCharacters"/> is provided then all characters that are not present in this string will be removed</param>
        public TextNormalizer(Casing casing = null, string onlyValidCharacters = null)
        {
            _casing = casing;
            _onlyValidCharacters = onlyValidCharacters;
        }

        public string Normalize(string text)
        {
            text = ChangeCasing(text);

            return RemoveInvalidCharacters(text);
        }

        private string ChangeCasing(string text)
        {
            return _casing is not null
                ? _casing.Transform(text)
                : text;
        }

        private string RemoveInvalidCharacters(string text)
        {
            return _onlyValidCharacters is not null
                ? string.Concat(text.Where(_onlyValidCharacters.Contains))
                : text;
        }
    }
}