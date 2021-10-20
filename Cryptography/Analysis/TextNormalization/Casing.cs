using System;

namespace Cryptography.Analysis.TextNormalization
{
    public class Casing
    {
        public static readonly Casing None = new(text => text);

        public static readonly Casing UpperCase = new(text => text?.ToUpper());

        public static readonly Casing LowerCase = new(text => text?.ToLower());

        internal Func<string, string> Transform { get; }

        private Casing(Func<string, string> transform)
        {
            Transform = transform;
        }
    }
}