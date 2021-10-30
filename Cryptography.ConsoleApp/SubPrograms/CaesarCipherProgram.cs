using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Caesar;

namespace Cryptography.ConsoleApp.SubPrograms
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    internal static class CaesarCipherProgram
    {
        internal static void Run()
        {
            CaesarCipherBruteForceAttack attack = new(Alphabets.ALPHABET_);

            // THIS IS SECRETE MESSAGE
            attack.PrintAttack("WKLVCLVCVHFUHWHCPHVVDJH");
        }
    }
}