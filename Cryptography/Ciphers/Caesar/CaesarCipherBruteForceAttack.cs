using Cryptography.Utilities;

namespace Cryptography.Ciphers.Caesar
{
    /// <summary>
    /// Ciphertext only brute force attack. 
    /// </summary>
    public class CaesarCipherBruteForceAttack
    {
        private readonly AlphabetShifting _alphabetShifting;

        private int AlphabetLength => _alphabetShifting.Alphabet.Length;

        public CaesarCipherBruteForceAttack(string alphabet)
        {
            _alphabetShifting = new AlphabetShifting(alphabet);
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText, out int? shift)
        {
            for (shift = 0; shift < AlphabetLength; shift++)
            {
                decryptedText = _alphabetShifting.ShiftEveryChar(encryptedText, -shift.Value);

                if (attackChecker.IsDecryptedCorrectly(decryptedText))
                {
                    return true;
                }
            }

            decryptedText = null;
            shift = null;
            return false;
        }
    }
}