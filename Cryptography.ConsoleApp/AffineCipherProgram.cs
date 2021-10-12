using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Ciphers.Affine;

namespace Cryptography.ConsoleApp
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    internal static class AffineCipherProgram
    {
        internal static void Run()
        {
            AffineCipherBruteForceAttack attack = new(Alphabets.ALPHABET_);

            // VYRIESIL SOM LAHKU ULOHU
            attack.PrintAttack("LIYGTOGDPOAUPDFQNVPVDAQV");
        }
    }
}