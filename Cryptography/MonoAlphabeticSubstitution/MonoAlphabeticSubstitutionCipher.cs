using System;
using System.Text;
using Cryptography.Abstraction;

namespace Cryptography.MonoAlphabeticSubstitution
{
    public class MonoAlphabeticSubstitutionCipher : ICipher
    {
        private readonly string _originalAlphabet;

        private readonly string _substitutionAlphabet;

        public MonoAlphabeticSubstitutionCipher(string originalAlphabet, string substitutionAlphabet)
        {
            _originalAlphabet = !string.IsNullOrEmpty(originalAlphabet)
                ? originalAlphabet
                : throw new ArgumentException("Value cannot be null or empty.", nameof(originalAlphabet));

            _substitutionAlphabet = !string.IsNullOrEmpty(substitutionAlphabet)
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

        public string Encrypt(string plainText) => SubstituteEveryChar(plainText, EncryptSubstitution);

        public string Decrypt(string encryptedText) => SubstituteEveryChar(encryptedText, DecryptSubstitution);

        private string SubstituteEveryChar(string text, Func<char, char> substitution)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));

            var stringBuilder = new StringBuilder(text.Length);

            foreach (char ch in text)
            {
                char substituted = substitution(ch);
                stringBuilder.Append(substituted);
            }

            return stringBuilder.ToString();
        }

        private char EncryptSubstitution(char ch)
        {
            int charIndex = GetCharIndex(ch, _originalAlphabet);
            return _substitutionAlphabet[charIndex];
        }

        private char DecryptSubstitution(char ch)
        {
            int charIndex = GetCharIndex(ch, _substitutionAlphabet);
            return _originalAlphabet[charIndex];
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