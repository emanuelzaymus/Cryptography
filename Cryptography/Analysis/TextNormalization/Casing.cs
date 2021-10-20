using System;

namespace Cryptography.Analysis.TextNormalization
{
    internal class Casing
    {
        internal static readonly Casing None = new(text => text);

        internal static readonly Casing UpperCase = new(text => text?.ToUpper());

        internal static readonly Casing LowerCase = new(text => text?.ToLower());

        internal Func<string, string> Transform { get; }

        private Casing(Func<string, string> transform)
        {
            Transform = transform;
        }
    }
}