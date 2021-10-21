using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cryptography.Alphabet;
using Cryptography.Analysis;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Vigenere
{
    public class VigenereCipherKasiskiAttack
    {
        private readonly string _alphabet;
        private readonly List<LetterProbability> _probabilitiesOfLetters;

        public VigenereCipherKasiskiAttack(string alphabet, List<LetterProbability> probabilitiesOfLetters)
        {
            Alphabets.CheckAlphabet(alphabet);

            _alphabet = alphabet;
            _probabilitiesOfLetters = probabilitiesOfLetters;
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out string password, int tryKeysCount)
        {
            return Attack(encryptedText, attackChecker, out decryptedText, out password, false, tryKeysCount);
        }

        public void PrintAttack(string encryptedText, int tryKeysCount)
        {
            Attack(encryptedText, null, out _, out _, true, tryKeysCount);
        }

        private bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out string password, bool print, int tryKeysCount)
        {
            var passwordLengthEstimations = KasiskiExamination.GetPasswordLengthEstimations(encryptedText, 3, 8);

            foreach (var passwordLength in passwordLengthEstimations.Select(e => e.Length))
            {
                var strings = DivideTextIntoNStringsCyclically(encryptedText, passwordLength);

                var coincidences = strings.Select(s => IndexOfCoincidence.GetIndexOfCoincidence(s, _alphabet)).ToList();

                bool allCoincidencesAreAboveThreshold = coincidences
                    .Select(c => c > IndexOfCoincidence.Threshold).All(b => b);

                // I chose suitable passwordLength 
                if (allCoincidencesAreAboveThreshold)
                {
                    List<List<LetterProbability>> allLetterProbabilities = strings
                        .Select(s => LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(s, _alphabet))
                        .ToList();

                    // Calculate ofr every string the smallest differences of letter probabilities (take 5 smallest)
                    List<List<(int Shift, double Difference)>> allDifferences = allLetterProbabilities
                        .Select(AllLettersProbabilitiesDifferenceForEveryShift)
                        .ToList();

                    //var currentIndices = Enumerable.Repeat(0, 4).ToList();

                    // Teraz vyskusam len najlepsie sifty

                    var bestShifts = allDifferences.Select(differences => differences.First().Shift).ToList();

                    decryptedText = TryShifts(encryptedText, bestShifts);
                    password = string.Concat(bestShifts.Select(s => _alphabet[s]));

                    if (print)
                    {
                        Console.WriteLine(decryptedText);
                    }

                    if (attackChecker.IsDecryptedCorrectly(decryptedText))
                    {
                        return true;
                    }
                }
            }

            decryptedText = null;
            password = null;
            return false;
        }

        private string TryShifts(string encryptedText, IReadOnlyList<int> shifts)
        {
            return encryptedText.Transform((ch, i) =>
            {
                int charIndex = _alphabet.GetCharIndex(ch);
                int shift = shifts[i % shifts.Count];
                return _alphabet[Utils.PositiveModulo(charIndex - shift, _alphabet.Length)];
            });
        }

        private List<string> DivideTextIntoNStringsCyclically(string text, int n)
        {
            var stringBuilders = new List<StringBuilder>(n);

            foreach (var i in Enumerable.Range(0, n))
            {
                stringBuilders.Add(new StringBuilder(text.Length / n + 1));
            }

            foreach ((char ch, int i) in text.Select((ch, i) => (ch, i)))
            {
                stringBuilders[i % n].Append(ch);
            }

            return stringBuilders.Select(b => b.ToString()).ToList();
        }

        private List<(int Shift, double Difference)> AllLettersProbabilitiesDifferenceForEveryShift(
            List<LetterProbability> letterProbabilities)
        {
            return Enumerable.Range(0, _alphabet.Length)
                .Select(i => (Shift: i, Difference: AllLettersProbabilitiesDifference(letterProbabilities, i)))
                .OrderBy(tuple => tuple.Difference)
                .ToList();
        }

        private double AllLettersProbabilitiesDifference(List<LetterProbability> guessProbabilities, int shift)
        {
            double sum = 0;

            foreach (var (lp, i) in _probabilitiesOfLetters.Select((lp, i) => (letterProbability: lp, i)))
            {
                var realProbability = lp.Probability;
                var guessProbability = guessProbabilities[(i + shift) % guessProbabilities.Count].Probability;

                sum += Math.Pow(realProbability - guessProbability, 2);
            }

            return sum;
        }
    }
}