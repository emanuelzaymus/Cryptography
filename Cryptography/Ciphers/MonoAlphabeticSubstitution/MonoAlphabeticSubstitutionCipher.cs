using System;
using Cryptography.Alphabet;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.MonoAlphabeticSubstitution
{
    /// <summary>
    /// Mono-alphabetic substitution.
    /// </summary>
    public class MonoAlphabeticSubstitutionCipher : Cipher
    {
        private string OriginalAlphabet => Alphabet;

        private string SubstitutionAlphabet { get; }

        public MonoAlphabeticSubstitutionCipher(string originalAlphabet, string substitutionAlphabet)
            : base(originalAlphabet)
        {
            Alphabets.CheckAlphabet(substitutionAlphabet);
            SubstitutionAlphabet = substitutionAlphabet;

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

        protected override char CharEncryption(char ch, int stringCharIndex)
        {
            int alphabetCharIndex = OriginalAlphabet.GetCharIndex(ch);
            return SubstitutionAlphabet[alphabetCharIndex];
        }

        protected override char CharDecryption(char ch, int stringCharIndex)
        {
            int alphabetCharIndex = SubstitutionAlphabet.GetCharIndex(ch);
            return OriginalAlphabet[alphabetCharIndex];
        }
    }
}