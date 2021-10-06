using System;

namespace Cryptography.ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Caesar.CaesarCipher cipher = new(Alphabets.Alphabets.ALPHABET, 356);

            const string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Console.WriteLine(text);

            var encrypted = cipher.Encrypt(text);
            Console.WriteLine(encrypted);

            var decrypted = cipher.Decrypt(encrypted);
            Console.WriteLine(decrypted);

            Console.WriteLine(text == decrypted ? "ARE EQUAL" : "ARE NOT EQUAL !!!");
        }
    }
}