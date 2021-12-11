using System;
using System.Diagnostics.CodeAnalysis;

namespace Cryptography.Alphabet
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Alphabets
    {
        public const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string ALPHABET123 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string ALPHABET_ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        public const string ALPHABET_123 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789";

        public const string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public const string alphabet123 = "abcdefghijklmnopqrstuvwxyz0123456789";
        public const string alphabet_ = "abcdefghijklmnopqrstuvwxyz ";
        public const string alphabet_123 = "abcdefghijklmnopqrstuvwxyz 0123456789";

        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const string Alphabet123 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public const string Alphabet_ = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
        public const string Alphabet_123 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789";

        public static void CheckAlphabet(string alphabetString)
        {
            if (string.IsNullOrEmpty(alphabetString))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(alphabetString));
            }
        }
    }
}