using System;
using Cryptography.Alphabet;
using Cryptography.Utilities;

namespace Cryptography.Ciphers
{
    public abstract class Attack
    {
        protected string Alphabet { get; }

        protected Attack(string alphabet)
        {
            Alphabets.CheckAlphabet(alphabet);

            Alphabet = alphabet;
        }

        protected void PrintResult(bool print, string decryptedText, string formattedText,
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