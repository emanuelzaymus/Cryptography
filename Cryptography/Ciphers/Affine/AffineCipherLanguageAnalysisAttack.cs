using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Analysis;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public class AffineCipherLanguageAnalysisAttack : AffineCipherAttack
    {
        private readonly Dictionary<char, double> _lettersProbabilities;

        public AffineCipherLanguageAnalysisAttack(string alphabet, Dictionary<char, double> lettersProbabilities)
            : base(alphabet)
        {
            if (alphabet.Length != lettersProbabilities.Count)
                throw new Exception("Alphabet length and lettersProbabilities count do not equal.");

            _lettersProbabilities = lettersProbabilities;
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
            char mostProbableChar = MostProbableCharacter(_lettersProbabilities);
            int mostProbableCharIndex = Alphabet.GetCharIndex(mostProbableChar);

            var encryptedProbabilities = LanguageFrequencyAnalysis
                .GetLettersProbabilities(encryptedText, Alphabet)
                .OrderByDescending(pair => pair.Value)
                .Select(pair => pair.Key)
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

                    if (print)
                    {
                        PrintResult(decryptKey1, decryptKey2, decryptedText);
                    }

                    if (attackChecker is not null && attackChecker.IsDecryptedCorrectly(decryptedText))
                    {
                        return true;
                    }
                }

                Console.WriteLine();
            }

            decryptedText = null;
            decryptKey1 = null;
            decryptKey2 = null;

            return false;
        }

        private char MostProbableCharacter(Dictionary<char, double> lettersProbabilities)
        {
            return lettersProbabilities
                .Aggregate((pair1, pari2) => pair1.Value > pari2.Value
                    ? pair1
                    : pari2)
                .Key;
        }
    }
}