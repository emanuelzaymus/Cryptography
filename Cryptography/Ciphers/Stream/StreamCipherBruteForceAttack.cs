using Cryptography.Analysis;
using Cryptography.RandomNumberGenerators;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Stream
{
    public class StreamCipherBruteForceAttack : Attack
    {
        private readonly IRng _rng;

        public StreamCipherBruteForceAttack(string alphabet, IRng rng) : base(alphabet)
        {
            _rng = rng;
        }

        public bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? usedSeed, double indexOfCoincidenceThreshold)
        {
            return Attack(encryptedText, attackChecker, out decryptedText, out usedSeed, false, null, null,
                indexOfCoincidenceThreshold);
        }

        public void PrintAttack(string encryptedText, string formattedText, string writeToFile,
            double indexOfCoincidenceThreshold)
        {
            Attack(encryptedText, null, out _, out _, true, formattedText, writeToFile, indexOfCoincidenceThreshold);
        }

        private bool Attack(string encryptedText, AttackChecker attackChecker, out string decryptedText,
            out int? usedSeed, bool print, string formattedText, string writeToFile, double indexOfCoincidenceThreshold)
        {
            DeleteFile(writeToFile);

            StreamCipher streamCipher = new(Alphabet, _rng);
            var decryptedTextArray = new char[encryptedText.Length];

            for (usedSeed = 0; usedSeed < _rng.PeriodLength; usedSeed++)
            {
                _rng.SetSeedAndRestart(usedSeed.Value);

                streamCipher.Decrypt(encryptedText, decryptedTextArray);

                double indexOfCoincidence = IndexOfCoincidence.GetIndexOfCoincidence(decryptedTextArray, Alphabet);

                if (indexOfCoincidence > indexOfCoincidenceThreshold)
                {
                    decryptedText = new string(decryptedTextArray);

                    PrintResult(print, decryptedText, formattedText, writeToFile,
                        $"Index of coincidence: {indexOfCoincidence}", $"Used seed: {usedSeed}");

                    if (attackChecker is not null && attackChecker.IsDecryptedCorrectly(decryptedText))
                    {
                        return true;
                    }
                }
            }

            decryptedText = null;
            usedSeed = null;
            return false;
        }
    }
}