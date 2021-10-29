using System;
using System.IO;
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

        protected void DeleteFile(string writeToFile)
        {
            if (File.Exists(writeToFile))
            {
                File.Delete(writeToFile);
            }
        }

        protected void PrintResult(bool print, string decryptedText, string formattedText, string writeToFile,
            params object[] additionalParameters)
        {
            if (print)
            {
                var output = formattedText is not null
                    ? Utils.FormatString(decryptedText, formattedText, Alphabet)
                    : decryptedText;

                output += "\n";

                foreach (var parameter in additionalParameters)
                {
                    output += parameter + "\n";
                }

                output += "\n\n";

                Console.WriteLine(output);

                // TODO: writing does not work
                if (writeToFile != null)
                {
                    File.AppendAllText(writeToFile, output);
                }
            }
        }
    }
}