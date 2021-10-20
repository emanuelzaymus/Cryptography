using System;
using System.Collections.Generic;
using System.Linq;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Affine
{
    public class AffineCipherLanguageAnalysisAttack
    {
        private readonly string _alphabet;
        private readonly Dictionary<char, double> _lettersProbabilities;

        private readonly int[] _divisors;

        public AffineCipherLanguageAnalysisAttack(string alphabet, Dictionary<char, double> lettersProbabilities)
        {
            Alphabets.CheckAlphabet(alphabet);

            if (alphabet.Length != lettersProbabilities.Count)
            {
                throw new Exception("Alphabet length and lettersProbabilities count do not equal.");
            }

            _alphabet = alphabet;
            _lettersProbabilities = lettersProbabilities;

            _divisors = Utils.GetDivisorsWithout1(alphabet.Length);
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
            int mostProbableCharIndex = _alphabet.GetCharIndex(mostProbableChar);

            var encryptedProbabilities = LanguageFrequencyAnalysis
                .GetLettersProbabilities(encryptedText, _alphabet)
                .OrderByDescending(pair => pair.Value)
                .Select(pair => pair.Key)
                .ToList();

            for (int i = 0; i < tryKeysCount; i++)
            {
                char mostProbableEncryptedChar = encryptedProbabilities[i];
                int mostProbableEncryptedCharIndex = _alphabet.GetCharIndex(mostProbableEncryptedChar);

                for (decryptKey1 = 0; decryptKey1 < _alphabet.Length; decryptKey1++)
                {
                    if (IsDivisibleByAlphabetDivisors(decryptKey1.Value))
                    {
                        continue;
                    }

                    decryptKey2 = Utils.PositiveModulo(
                        mostProbableCharIndex - mostProbableEncryptedCharIndex * decryptKey1.Value,
                        _alphabet.Length);

                    decryptedText = TryDecrypt(encryptedText, decryptKey1.Value, decryptKey2.Value);

                    if (print)
                    {
                        Console.WriteLine("{0,3} {1,3}  {2}", decryptKey1, decryptKey2, decryptedText);
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
            return lettersProbabilities.Aggregate((pair1, pari2) => pair1.Value > pari2.Value ? pair1 : pari2).Key;
        }

        private bool IsDivisibleByAlphabetDivisors(int i)
        {
            return _divisors.Any(d => i % d == 0);
        }

        private string TryDecrypt(string encryptedText, int decryptKey1, int decryptKey2)
        {
            return encryptedText.Transform((ch, _) =>
            {
                int alphabetCharIndex = _alphabet.GetCharIndex(ch);
                int newCharIndex =
                    Utils.PositiveModulo(alphabetCharIndex * decryptKey1 + decryptKey2, _alphabet.Length);
                return _alphabet[newCharIndex];
            });
        }
    }
}