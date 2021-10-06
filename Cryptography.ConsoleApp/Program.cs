using System;

namespace Cryptography.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CaesarCipher cipher = new(Alphabets.ALPHABET, 356);

            //string text = "TOTOJETAJNASPRAVA";
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Console.WriteLine(text);

            string encrypted = cipher.Encrypt(text);

            Console.WriteLine(encrypted);

            string decripted = cipher.Decrypt(encrypted);

            Console.WriteLine(decripted);

            Console.WriteLine(text == decripted ? "ARE EAQUAL" : "ARE NOT EQUAL !!!");
        }
    }
}
