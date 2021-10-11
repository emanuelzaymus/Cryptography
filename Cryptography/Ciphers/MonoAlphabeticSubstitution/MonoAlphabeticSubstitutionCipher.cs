using System;

namespace Cryptography.Ciphers.MonoAlphabeticSubstitution
{
    public class MonoAlphabeticSubstitutionCipher : Cipher
    {
        private string OriginalAlphabet => Alphabet;

        private string SubstitutionAlphabet { get; }

        public MonoAlphabeticSubstitutionCipher(string originalAlphabet, string substitutionAlphabet)
            : base(originalAlphabet)
        {
            SubstitutionAlphabet = !string.IsNullOrEmpty(substitutionAlphabet)
                ? substitutionAlphabet
                : throw new ArgumentException("Value cannot be null or empty.", nameof(substitutionAlphabet));

            CheckAlphabets(originalAlphabet, substitutionAlphabet);
        }

        private void CheckAlphabets(string originalAlphabet, string substitutionAlphabet)
        {
            if (originalAlphabet.Length != substitutionAlphabet.Length)
                throw new ArgumentException("Original and Substitution alphabets are not of the same length.");

            foreach (char ch in originalAlphabet)
            {
                if (!substitutionAlphabet.Contains(ch))
                    throw new ArgumentException($"Substitution alphabet does not contain character '{ch}'.");
            }
        }

        protected override char CharEncryption(char ch)
        {
            int charIndex = GetCharIndex(ch, OriginalAlphabet);
            return SubstitutionAlphabet[charIndex];
        }

        protected override char CharDecryption(char ch)
        {
            int charIndex = GetCharIndex(ch, SubstitutionAlphabet);
            return OriginalAlphabet[charIndex];
        }

        private int GetCharIndex(char ch, string alphabet)
        {
            int index = alphabet.IndexOf(ch);

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ch),
                    $"Cannot find character '{ch}' in the alphabet. The character is not valid.");
            }

            return index;
        }
    }
}