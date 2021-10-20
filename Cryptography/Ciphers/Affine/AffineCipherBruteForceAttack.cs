using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    /// <summary>
    /// Ciphertext only brute force attack.
    /// </summary>
    public class AffineCipherBruteForceAttack : AffineCipherAttack
    {
        public AffineCipherBruteForceAttack(string alphabet) : base(alphabet)
        {
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? decryptKey1, out int? decryptKey2)
        {
            return Attack(encryptedText, attackChecker, out decryptedText, out decryptKey1, out decryptKey2, false);
        }

        public void PrintAttack(string encryptedText)
        {
            Attack(encryptedText, null, out _, out _, out _, true);
        }

        private bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? decryptKey1, out int? decryptKey2, bool print)
        {
            for (decryptKey1 = 0; decryptKey1 < Alphabet.Length; decryptKey1++)
            {
                if (IsDivisibleByAlphabetDivisors(decryptKey1.Value))
                {
                    continue;
                }

                for (decryptKey2 = 0; decryptKey2 < Alphabet.Length; decryptKey2++)
                {
                    decryptedText = TryDecrypt(encryptedText, decryptKey1.Value, decryptKey2.Value);

                    PrintResult(print, decryptKey1, decryptKey2, decryptedText);

                    if (attackChecker is not null && attackChecker.IsDecryptedCorrectly(decryptedText))
                    {
                        return true;
                    }
                }

                PrintNewLine(print);
            }

            decryptedText = null;
            decryptKey1 = null;
            decryptKey2 = null;

            return false;
        }
    }
}