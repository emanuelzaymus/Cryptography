using Cryptography.Utilities;

namespace Cryptography.Ciphers.Caesar
{
    internal static class CaesarCipherUtils
    {
        internal static char ShiftChar(char ch, int shift, string alphabet)
        {
            int charIndex = alphabet.GetCharIndex(ch);
            int newCharIndex = ShiftIndex(charIndex, shift, alphabet.Length);
            return alphabet[newCharIndex];
        }

        private static int ShiftIndex(int index, int shift, int alphabetLength)
        {
            return Utils.PositiveModulo(index + shift, alphabetLength);
        }
    }
}