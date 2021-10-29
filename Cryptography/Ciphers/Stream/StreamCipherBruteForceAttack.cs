using System;
using Cryptography.RandomNumberGenerators;
using Cryptography.Utilities;

namespace Cryptography.Ciphers.Stream
{
    public class StreamCipherBruteForceAttack : Attack
    {
        public StreamCipherBruteForceAttack(string alphabet) : base(alphabet)
        {
        }

        public bool Attack(string encryptedText, IRng rng, AttackChecker attackChecker, out string decryptedText,
            out int? usedSeed)
        {
            return Attack(encryptedText, rng, attackChecker, out decryptedText, out usedSeed, false, null);
        }

        public void PrintAttack(string encryptedText, IRng rng, string formattedText)
        {
            Attack(encryptedText, rng, null, out _, out _, true, formattedText);
        }

        private bool Attack(string encryptedText, IRng rng, AttackChecker attackChecker, out string decryptedText,
            out int? usedSeed, bool print, string formattedText)
        {
            StreamCipher streamCipher = new(Alphabet, rng);

            for (usedSeed = 0; usedSeed < rng.PeriodLength; usedSeed++)
            {
                rng.SetSeedAndRestart(usedSeed.Value);
                decryptedText = streamCipher.Decrypt(encryptedText);

                PrintResult(print, decryptedText, formattedText, usedSeed);

                if (attackChecker is not null && attackChecker.IsDecryptedCorrectly(decryptedText))
                {
                    return true;
                }
            }

            decryptedText = null;
            usedSeed = null;
            return false;
        }

        // TODO: put into antecedent
        private void PrintResult(bool print, string decryptedText, string formattedText,
            params object[] additionalParameters)
        {
            if (print)
            {
                if (formattedText is not null)
                {
                    var formatted = Utils.FormatString(decryptedText, formattedText, Alphabet);

                    Console.WriteLine(formatted);
                }
                else
                {
                    Console.WriteLine(decryptedText);
                }

                foreach (var parameter in additionalParameters)
                {
                    Console.WriteLine(parameter);
                }

                Console.WriteLine("\n\n");
            }
        }
    }
}