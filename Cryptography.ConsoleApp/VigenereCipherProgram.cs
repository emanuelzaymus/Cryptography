using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Ciphers.Vigenere;
using Cryptography.Utilities;

namespace Cryptography.ConsoleApp
{
    internal static class VigenereCipherProgram
    {
        internal static void Run()
        {
            VigenereCipherKasiskiAttack attack = new(Alphabets.ALPHABET, ProbabilitiesOfLetters.SlovakLanguage);

            attack.PrintAttack(EncryptedTexts.VigenereEncryptedText, 5);
        }
    }
}