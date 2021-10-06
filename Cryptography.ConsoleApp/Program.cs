using System;

namespace Cryptography.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CaesarCipher cipher = new(Alphabets.ALPHABET, 356);

            //string text = "TOTOJETAJNASPRAVA";
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
