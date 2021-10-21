using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Analysis;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public class AffineCipherLanguageAnalysisAttack : AffineCipherAttack
    {
        private readonly List<LetterProbability> _probabilitiesOfLetters;

        public AffineCipherLanguageAnalysisAttack(string alphabet, List<LetterProbability> probabilitiesOfLetters)
            : base(alphabet)
        {
            if (alphabet.Length != probabilitiesOfLetters.Count)
                throw new Exception("Alphabet length and lettersProbabilities count do not equal.");

            _probabilitiesOfLetters = probabilitiesOfLetters;
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? decryptKey1, out int? decryptKey2, int tryKeysCount)
        {
            return Attack(encryptedText, attackChecker, out decryptedText, out decryptKey1, out decryptKey2, false,
                tryKeysCount);
        }

        public void PrintAttack(string encryptedText, int tryKeysCount)
        {
            Attack(encryptedText, null, out _, out _, out _, true, tryKeysCount);
        }

        private bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? decryptKey1, out int? decryptKey2, bool print, int tryKeysCount)
        {
            char mostProbableChar = MostProbableCharacter();
            int mostProbableCharIndex = Alphabet.GetCharIndex(mostProbableChar);

            var encryptedProbabilities = LanguageFrequencyAnalysis
                .GetProbabilitiesOfLetters(encryptedText, Alphabet)
                .OrderByDescending(letterProbability => letterProbability.Probability)
                .Select(letterProbability => letterProbability.Letter)
                .ToList();

            for (int i = 0; i < tryKeysCount; i++)
            {
                char mostProbableEncryptedChar = encryptedProbabilities[i];
                int mostProbableEncryptedCharIndex = Alphabet.GetCharIndex(mostProbableEncryptedChar);

                for (decryptKey1 = 0; decryptKey1 < Alphabet.Length; decryptKey1++)
                {
                    if (IsDivisibleByAlphabetDivisors(decryptKey1.Value))
                    {
                        continue;
                    }

                    decryptKey2 = Utils.PositiveModulo(
                        mostProbableCharIndex - mostProbableEncryptedCharIndex * decryptKey1.Value,
                        Alphabet.Length);

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

        private char MostProbableCharacter()
        {
            return _probabilitiesOfLetters
                .Aggregate((lp1, lp2) => lp1.Probability > lp2.Probability
                    ? lp1
                    : lp2)
                .Letter;
        }
    }
}