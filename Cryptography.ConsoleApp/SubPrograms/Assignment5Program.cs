using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cryptography.Alphabet;
using Cryptography.Hashes;
using Cryptography.Utilities;

namespace Cryptography.ConsoleApp.SubPrograms
{
    internal static class Assignment5Program
    {
        internal static void RunShadow1() => RunShadow(Paths.In.Assignment5.Shadow1);

        internal static void RunShadow2() => RunShadow(Paths.In.Assignment5.Shadow2);

        internal static void RunShadow3() => RunShadow(Paths.In.Assignment5.Shadow3);

        internal static void RunShadow4() => RunShadow(Paths.In.Assignment5.Shadow4);

        private static void RunShadow(string shadowFilePath)
        {
            Console.WriteLine(shadowFilePath);
            var shadows = ShadowLoader.LoadUserShadows(shadowFilePath).ToList();

            // ReSharper disable once JoinDeclarationAndInitializer
            Md5Attack attack;

            // attack = new Md5DictionaryAttack(SlovakFirstNames.GetNamesWithDiminutives());
            // RunAttack(attack, shadows);

            attack = new Md5BruteForceAttack(Alphabets.alphabet, 6, 7);
            RunAttack(attack, shadows);

            attack = new Md5BruteForceAttack(Alphabets.Alphabet123, 4, 5);
            RunAttack(attack, shadows);
        }

        private static void RunAttack(Md5Attack md5Attack, List<UserShadow> shadows)
        {
            var stopwatch = Stopwatch.StartNew();

            var crackedPasswords = md5Attack.CrackPasswords(shadows);

            foreach (var crackedPassword in crackedPasswords)
            {
                Console.WriteLine($"    {crackedPassword}");
            }

            stopwatch.Stop();
            Console.WriteLine($"    ElapsedMilliseconds: {stopwatch.ElapsedMilliseconds}\n");
        }
    }
}