using System;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Caesar
{
    /// <summary>
    /// Ciphertext only brute force attack.
    /// </summary>
    public class CaesarCipherBruteForceAttack
    {
        private readonly string _alphabet;

        public CaesarCipherBruteForceAttack(string alphabet)
        {
            _alphabet = !string.IsNullOrEmpty(alphabet)
                ? alphabet
                : throw new ArgumentException("Value cannot be null or empty.", nameof(alphabet));
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText, out int? shift)
        {
            for (shift = 0; shift < _alphabet.Length; shift++)
            {
                decryptedText = ShiftEveryChar(encryptedText, -shift.Value);

                if (attackChecker.IsDecryptedCorrectly(decryptedText))
                {
                    return true;
                }
            }

            decryptedText = null;
            shift = null;

            return false;
        }

        private string ShiftEveryChar(string encryptedText, int shift)
        {
            return encryptedText.Transform(ch => CaesarCipherUtils.ShiftChar(ch, shift, _alphabet));
        }
    }
}