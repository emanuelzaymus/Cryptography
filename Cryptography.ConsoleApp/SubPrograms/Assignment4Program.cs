using System;
using System.Diagnostics;
using System.Numerics;
using Cryptography.Ciphers.RSA;

namespace Cryptography.ConsoleApp.SubPrograms
{
    internal static class Assignment4Program
    {
        internal static void RunAll()
        {
            Run(1, 13169004533, 65537, 6029832903);
            Run(2, 1690428486610429, 65537, 22496913456008);
            Run(3, BigInteger.Parse("56341958081545199783"), 65537, 17014716723435111315);
        }

        private static void Run(int numberOfTask, BigInteger module, BigInteger publicKey, BigInteger encryptedMessage)
        {
            var attack = new RsaCipherAttack(publicKey, module);

            var stopwatch = Stopwatch.StartNew();

            attack.CrackPrivateKeyParallel();
            var decryptedMessage = attack.Attack(encryptedMessage);

            stopwatch.Stop();

            Console.WriteLine(numberOfTask + ".");
            Console.WriteLine("    n = " + module);
            Console.WriteLine("    e = " + publicKey);
            Console.WriteLine("    y = " + encryptedMessage);
            Console.WriteLine("    p = " + attack.PrimeP);
            Console.WriteLine("    q = " + attack.PrimeQ);
            Console.WriteLine("    phi(n) = " + attack.Phi);
            Console.WriteLine("    d = " + attack.PrivateKey);
            Console.WriteLine("    x = " + decryptedMessage);

            Console.WriteLine($"ElapsedMilliseconds: {stopwatch.ElapsedMilliseconds}\n");
        }
    }
}