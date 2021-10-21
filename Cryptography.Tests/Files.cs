using System.IO;

namespace Cryptography.Tests
{
    internal static class Files
    {
        internal static readonly FileInfo EnTelegraph = new("Resources/TextsToAnalyse/en_teleg.txt");
        internal static readonly FileInfo SkTelegraph = new("Resources/TextsToAnalyse/sk_teleg.txt");

        internal static readonly FileInfo Text1 = new("Resources/Texts/text1.txt");
        internal static readonly FileInfo Text2 = new("Resources/Texts/text2.txt");
    }
}