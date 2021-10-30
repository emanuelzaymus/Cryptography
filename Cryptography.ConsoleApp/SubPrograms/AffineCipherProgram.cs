using System.Diagnostics.CodeAnalysis;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Ciphers.Affine;

namespace Cryptography.ConsoleApp.SubPrograms
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    internal static class AffineCipherProgram
    {
        internal static void Run()
        {
            AffineCipherLanguageAnalysisAttack attack = new(Alphabets.ALPHABET_,
                ProbabilitiesOfLetters.SkTelegraphWithSpace);

            // VYRIESIL SOM LAHKU ULOHU
            attack.PrintAttack("LIYGTOGDPOAUPDFQNVPVDAQV", 2);
        }
    }
}