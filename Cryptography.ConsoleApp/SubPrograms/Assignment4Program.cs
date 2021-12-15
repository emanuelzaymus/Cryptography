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

/*
Console output:

1.
    n = 13169004533
    e = 65537
    y = 6029832903
    p = 101279
    q = 130027
    phi(n) = 13168773228
    d = 72739001
    x = 1234567890
ElapsedMilliseconds: 49

2.
    n = 1690428486610429
    e = 65537
    y = 22496913456008
    p = 35352181
    q = 47816809
    phi(n) = 1690428403441440
    d = 1308297747522113
    x = 1234567890
ElapsedMilliseconds: 148

3.
    n = 56341958081545199783	
    e = 65537
    y = 17014716723435111315
    p = 6940440583
    q = 8117922401
    phi(n) = 56341958066486836800
    d = 10931906232715055873
    x = 1234567890
ElapsedMilliseconds: 75615

 */

/*
Other tasks were counted using WolframAlpha:
 
https://www.wolframalpha.com/input/?i=factorize+6120215756887394998931731
https://www.wolframalpha.com/input/?i=%282092777627483++-+1%29+*+%282924446284457+-+1%29
https://www.wolframalpha.com/widgets/view.jsp?id=a9d64f006accc458a887ceb71eca63c6
https://www.wolframalpha.com/input/?i=%285077587957348826939798388%5E4628379897502241593077665%29+mod+6120215756887394998931731
	
4.
	n = 6120215756887394998931731; e = 65537; y = 5077587957348826939798388;
	p = 2092777627483
	q = 2924446284457
	phi(n) = 6120215756882377775019792
	d = 4628379897502241593077665
	x = 1234567890
	
5.
	n = 514261067785300163931552303017; e = 65537; y = 357341101854457993054768343508;
	p = 605742134588197
	q = 848976880459061
	phi(n) = 514261067785298709212537255760
	d = 138834873007999909396179588113
	x = 1234567890
	
6.
	n = 21259593755515403367535773703117421; e = 65537; y = 18829051270422357250395121195166553;
	p = 120913567052497781
	q = 175824717389116441
	phi(n) = 21259593755515403070797489261503200
	d = 16561767538761904020317771857821473
	x = 1234567890
	
7.
	n = 1371108864054663830856429909460283182291; e = 65537; y = 35962927026249687666434209737424754460;
	p = 29857785889724643173
	q = 45921317445260458967
	phi(n) = 1371108864054663830780650806125298080152
	d = 836678299148177608382742375124935850305
	x = 1234567890
 */