using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Cryptography.Analysis;
using Cryptography.Extensions;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Vigenere
{
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public class VigenereCipherKasiskiAttack : Attack
    {
        private readonly List<LetterProbability> _probabilitiesOfLetters;

        public VigenereCipherKasiskiAttack(string alphabet, List<LetterProbability> probabilitiesOfLetters) :
            base(alphabet)
        {
            _probabilitiesOfLetters = probabilitiesOfLetters;
        }

        /// <summary>
        /// Performs attack on <paramref name="encryptedText"/> using <c>Kasiski Examination</c> and language analysis.
        /// </summary>
        /// <param name="minPasswordLength">Minimal password length inclusive</param>
        /// <param name="maxPasswordLength">Maximal password length inclusive</param>
        /// <param name="tryCombinationsCount">How many combinations of generated shifts should be tried</param>
        /// <returns>Ture if success, false otherwise</returns>
        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out string password, int minPasswordLength, int maxPasswordLength, int tryCombinationsCount = 1)
        {
            return Attack(encryptedText, attackChecker, out decryptedText, out password, false, null,
                minPasswordLength, maxPasswordLength, tryCombinationsCount);
        }

        /// <summary>
        /// Performs attack on <paramref name="encryptedText"/> using <c>Kasiski Examination</c> and language analysis.
        /// </summary>
        /// <param name="formattedText">Original encrypted text without normalization containing formatting characters</param>
        /// <param name="minPasswordLength">Minimal password length inclusive</param>
        /// <param name="maxPasswordLength">Maximal password length inclusive</param>
        /// <param name="tryCombinationsCount">How many combinations of generated shifts should be tried</param>
        public void PrintAttack(string encryptedText, string formattedText, int minPasswordLength,
            int maxPasswordLength, int tryCombinationsCount = 1)
        {
            Attack(encryptedText, null, out _, out _, true,
                formattedText, minPasswordLength, maxPasswordLength, tryCombinationsCount);
        }

        private bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out string password, bool print, string formattedText, int minPasswordLength, int maxPasswordLength,
            int tryCombinationsCount)
        {
            var passwordLengthEstimations = KasiskiExamination.GetPasswordLengthEstimations(
                encryptedText, minPasswordLength, maxPasswordLength);

            foreach (var passwordLength in passwordLengthEstimations.Select(e => e.Length))
            {
                var dividedStrings = DivideTextIntoNStringsCyclically(encryptedText, passwordLength);

                if (!IsAverageCoincidenceAboveThreshold(dividedStrings))
                {
                    continue;
                }

                // Now I've chosen suitable passwordLength

                IEnumerable<List<LetterProbability>> allLetterProbabilities = dividedStrings
                    .Select(s => LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(s, Alphabet));

                // Calculate for every string the smallest differences of letter probabilities
                List<List<(int Shift, double Difference)>> allDifferences = allLetterProbabilities
                    .Select(AllLettersProbabilitiesDifferenceForEveryShift).ToList();

                var allIndicesCombinations = CreateAllIndicesCombinations(passwordLength, tryCombinationsCount);
                foreach (var indicesCombination in allIndicesCombinations)
                {
                    var shifts = SelectShifts(allDifferences, indicesCombination).ToList();

                    decryptedText = TryDecrypt(encryptedText, shifts);
                    password = CreatePasswordFromShifts(shifts);

                    PrintResult(print, decryptedText, formattedText, null, $"Password: {password}");

                    if (attackChecker is not null && attackChecker.IsDecryptedCorrectly(decryptedText))
                    {
                        return true;
                    }
                }
            }

            decryptedText = null;
            password = null;
            return false;
        }

        private List<string> DivideTextIntoNStringsCyclically(string text, int n)
        {
            var nStringBuilders = new List<StringBuilder>(n);

            for (int i = 0; i < n; i++)
            {
                nStringBuilders.Add(new StringBuilder(text.Length / n + 1));
            }

            foreach ((char ch, int i) in text.Select((ch, i) => (ch, i)))
            {
                nStringBuilders[i % n].Append(ch);
            }

            return nStringBuilders.Select(b => b.ToString()).ToList();
        }

        private bool IsAverageCoincidenceAboveThreshold(List<string> dividedStrings)
        {
            var coincidences = dividedStrings
                .Select(s => IndexOfCoincidence.GetIndexOfCoincidence(s, Alphabet));

            return coincidences.Average() > IndexOfCoincidence.Threshold;
        }

        private List<(int Shift, double Difference)> AllLettersProbabilitiesDifferenceForEveryShift(
            List<LetterProbability> letterProbabilities)
        {
            return Enumerable.Range(0, Alphabet.Length)
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

        private IEnumerable<List<int>> CreateAllIndicesCombinations(int passwordLength, int tryCombinationsCount)
        {
            return Permutations.GeneratePermutationSeries(passwordLength, tryCombinationsCount);
        }

        private IEnumerable<int> SelectShifts(List<List<(int Shift, double Difference)>> allDifferences,
            List<int> indicesCombination)
        {
            for (int i = 0; i < indicesCombination.Count; i++)
            {
                int iCombination = indicesCombination[i];
                yield return allDifferences[i][iCombination].Shift;
            }
        }

        private string TryDecrypt(string encryptedText, IReadOnlyList<int> shifts)
        {
            return encryptedText.Transform((ch, i) =>
            {
                int charIndex = Alphabet.GetCharIndex(ch);
                int shift = shifts[i % shifts.Count];
                return Alphabet[Utils.PositiveModulo(charIndex - shift, Alphabet.Length)];
            });
        }

        private string CreatePasswordFromShifts(List<int> shifts)
        {
            return string.Concat(shifts.Select(s => Alphabet[s]));
        }
    }
}