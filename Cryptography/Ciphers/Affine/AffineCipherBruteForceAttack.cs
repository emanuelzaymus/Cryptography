using System;
using System.Linq;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public class AffineCipherBruteForceAttack
    {
        private readonly string _alphabet;

        public AffineCipherBruteForceAttack(string alphabet)
        {
            _alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? decryptKey1, out int? decryptKey2)
        {
            for (decryptKey1 = 0; decryptKey1 < _alphabet.Length; decryptKey1++)
            {
                for (decryptKey2 = 0; decryptKey2 < _alphabet.Length; decryptKey2++)
                {
                    int decrypt1 = decryptKey1.Value;
                    int decrypt2 = decryptKey2.Value;

                    decryptedText = string.Concat(encryptedText.Select(ch =>
                    {
                        int charIndex = _alphabet.IndexOf(ch); // GetCharIndex(ch);
                        int newCharIndex = Utils.PositiveModulo(charIndex * decrypt1 + decrypt2, _alphabet.Length);
                        return _alphabet[newCharIndex];
                    }));

                    if (attackChecker.IsDecryptedCorrectly(decryptedText))
                    {
                        return true;
                    }
                }
            }

            decryptedText = null;
            decryptKey1 = null;
            decryptKey2 = null;
            return false;
        }
    }
}