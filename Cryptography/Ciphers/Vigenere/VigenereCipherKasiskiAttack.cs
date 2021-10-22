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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="attackChecker"></param>
        /// <param name="decryptedText"></param>
        /// <param name="password"></param>
        /// <param name="minPasswordLength"></param>
        /// <param name="maxPasswordLength"></param>
        /// <param name="tryCombinationsCount"></param>
        /// <returns></returns>
        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out string password, int minPasswordLength, int maxPasswordLength, int tryCombinationsCount)
        {
            return Attack(encryptedText, attackChecker, out decryptedText, out password, false, null,
                minPasswordLength, maxPasswordLength, tryCombinationsCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="formattedText"></param>
        /// <param name="minPasswordLength"></param>
        /// <param name="maxPasswordLength"></param>
        /// <param name="tryCombinationsCount"></param>
        public void PrintAttack(string encryptedText, string formattedText, int minPasswordLength,
            int maxPasswordLength, int tryCombinationsCount)
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
                    .Select(s => LanguageFrequencyAnalysis.GetProbabilitiesOfLetters(s, _alphabet));

                // Calculate for every string the smallest differences of letter probabilities (take 5 smallest)
                List<List<(int Shift, double Difference)>> allDifferences = allLetterProbabilities
                    .Select(AllLettersProbabilitiesDifferenceForEveryShift).ToList();

                var allIndicesCombinations = CreateAllIndicesCombinations(passwordLength, tryCombinationsCount);
                foreach (var indicesCombination in allIndicesCombinations)
                {
                    var shifts = SelectShifts(allDifferences, indicesCombination).ToList();

                    decryptedText = TryDecrypt(encryptedText, shifts);
                    password = CreatePasswordFromShifts(shifts);

                    PrintResult(print, decryptedText, formattedText, password);

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
                .Select(s => IndexOfCoincidence.GetIndexOfCoincidence(s, _alphabet));

            return coincidences.Average() > IndexOfCoincidence.Threshold;
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

        private IEnumerable<List<int>> CreateAllIndicesCombinations(int passwordLength, int tryCombinationsCount)
        {
            return Utils.GeneratePermutationSeries(passwordLength, tryCombinationsCount);
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
                int charIndex = _alphabet.GetCharIndex(ch);
                int shift = shifts[i % shifts.Count];
                return _alphabet[Utils.PositiveModulo(charIndex - shift, _alphabet.Length)];
            });
        }

        private string CreatePasswordFromShifts(List<int> shifts)
        {
            return string.Concat(shifts.Select(s => _alphabet[s]));
        }

        private void PrintResult(bool print, string decryptedText, string formattedText, string password)
        {
            if (print)
            {
                if (formattedText is not null)
                {
                    var formatted = Utils.FormatString(decryptedText, formattedText, _alphabet);

                    Console.WriteLine(formatted);
                }
                else
                {
                    Console.WriteLine(decryptedText);
                }

                Console.WriteLine($"Password: {password}\n");
            }
        }
    }
}