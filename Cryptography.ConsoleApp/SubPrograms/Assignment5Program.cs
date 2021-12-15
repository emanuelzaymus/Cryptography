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

            attack = new Md5DictionaryAttack(SlovakFirstNames.GetNamesWithDiminutives());
            RunAttack(attack, shadows, false);

            attack = new Md5BruteForceAttack(Alphabets.alphabet, 6, 7);
            RunAttack(attack, shadows, true);

            attack = new Md5BruteForceAttack(Alphabets.Alphabet123, 4, 5);
            RunAttack(attack, shadows, true);
        }

        private static void RunAttack(Md5Attack md5Attack, List<UserShadow> shadows, bool finishAfterFirstCracked)
        {
            var stopwatch = Stopwatch.StartNew();

            var crackedPasswords = md5Attack.CrackPasswords(shadows, finishAfterFirstCracked);

            foreach (var crackedPassword in crackedPasswords)
            {
                Console.WriteLine($"    {crackedPassword}");

                // Remove shadow from the list once it was cracked.
                shadows.Remove(crackedPassword);
            }

            stopwatch.Stop();
            Console.WriteLine($"    ElapsedMilliseconds: {stopwatch.ElapsedMilliseconds}\n");
        }
    }
}

// ReSharper disable CommentTypo

/*
Console output (8.78 hours):

./Resources/Assignment5/shadow1.txt
    scensna:cl9fi9Ds:w8HD2JUyVb9icKPplkxyvQ== -> maTusko
    ElapsedMilliseconds: 302
	
	urban14:0OPmlCkH:5C9B50Bkbt6/24BjwxfxOw== -> qhvkrls
    ElapsedMilliseconds: 15314204
	
	paluch:97sSrhFn:/eZB3PLEu8ZCYoHIViEc9Q== -> katkA
    ElapsedMilliseconds: 260411

./Resources/Assignment5/shadow2.txt
    pachnik:MKr1dNev:CgAO8fjImXcDPQAjKJ1I2g== -> maTusko
    sagan00:X4JMcpXS:/FyFniLICcgp8sn61S0JJA== -> kAtarina
    potkan3:mj9CUvQQ:pO+b3khbF+pZ1Womj+T9Cw== -> maTusko
    ElapsedMilliseconds: 263
	
	palenik:2wtkUhwc:0g94CIykFa202k3KIZPN8Q== -> zfiofdd
    ElapsedMilliseconds: 12813312
	
	fain:uHNo66sx:F2D2huLoLDdNqZ/ntmzYcw== -> Cl3I
    ElapsedMilliseconds: 916328

./Resources/Assignment5/shadow3.txt
    kysela9:tVGb9ad7:s2q3ZTl3346lC8r/Dv/TVg== -> milaDa
    potkan3:AkL4hFxE:SnkIl63J4nJeRVSU3WvU9w== -> maTusko
    kurka:t9hwjeNc:fjtKyIryL2aYYECaa/VXbA== -> adamKo
    ElapsedMilliseconds: 253
	
	vrab5:exW587DW:R/78RFTvs84OHY3f/dBLmw== -> djtlcy
    ElapsedMilliseconds: 758969
	
	poliakov:FE0Qmyww:kKg2T/JwhU1peV88bIW3Sg== -> D0C1
    ElapsedMilliseconds: 4057

./Resources/Assignment5/shadow4.txt
    slovakova5:VTDYVdhD:Xfm/7EcZwbHqkEhLC0OGQQ== -> adamKo
    ElapsedMilliseconds: 264
	
	slavikm:hl2r5cEr:A+ICyPRICZkm1OueW05LsA== -> xyzabc
    ElapsedMilliseconds: 1454479
	
	fratrikova7:eUSr3xnk:UPT+LU983/mKQE9ZyoyGnw== -> e1VL
    ElapsedMilliseconds: 78815

 */