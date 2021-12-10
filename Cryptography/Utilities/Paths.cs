namespace Cryptography.Utilities
{
    public static class Paths
    {
        private const string Resources = "Resources/";

        public static class In
        {
            private const string InResources = "./" + Resources;

            internal static class Assignment1
            {
                private const string Assignment1Directory = InResources + "Assignment1/";

                internal const string Text1 = Assignment1Directory + "text1_enc.txt";
                internal const string Text2 = Assignment1Directory + "text2_enc.txt";
                internal const string Text3 = Assignment1Directory + "text3_enc.txt";
                internal const string Text4 = Assignment1Directory + "text4_enc.txt";
            }

            internal static class Assignment3
            {
                private const string Assignment3Directory = InResources + "Assignment3/";

                internal const string Text1 = Assignment3Directory + "text1_enc.txt";
                internal const string Text2 = Assignment3Directory + "text2_enc.txt";
                internal const string Text3 = Assignment3Directory + "text3_enc.txt";
                internal const string Text4 = Assignment3Directory + "text4_enc.txt";
            }

            public static class Assignment5
            {
                private const string Assignment5Directory = InResources + "Assignment5/";

                public const string Shadow1 = Assignment5Directory + "shadow1.txt";
                public const string Shadow2 = Assignment5Directory + "shadow2.txt";
                public const string Shadow3 = Assignment5Directory + "shadow3.txt";
                public const string Shadow4 = Assignment5Directory + "shadow4.txt";

                public const string SlovakFemaleNames = Assignment5Directory + "slovak-female-names.txt";
                public const string SlovakMaleNames = Assignment5Directory + "slovak-male-names.txt";
            }

            internal static class Texts
            {
                private const string TextsDirectory = InResources + "Texts/";

                internal const string Text1 = TextsDirectory + "text1.txt";
                internal const string Text2 = TextsDirectory + "text2.txt";

                internal static class StreamCiphers
                {
                    private const string StreamCiphersDirectory = TextsDirectory + "StreamCiphers/";

                    internal const string Message = StreamCiphersDirectory + "spraca_enc.txt";
                }
            }

            internal static class TextsToAnalyse
            {
                private const string TextsToAnalyseDirectory = InResources + "TextsToAnalyse/";

                internal const string EnTelegraph = TextsToAnalyseDirectory + "en_teleg.txt";
                internal const string SkTelegraph = TextsToAnalyseDirectory + "sk_teleg.txt";
                internal const string SkWikipedia = TextsToAnalyseDirectory + "sk_wikipedia.txt";
            }
        }

        public static class Out
        {
            private const string OutResources = "../../../../Cryptography/" + Resources;

            public static class Assignment3
            {
                private const string Assignment3Directory = OutResources + "Assignment3/";

                public const string Text1 = Assignment3Directory + "text1_decrypted.txt";
                public const string Text2 = Assignment3Directory + "text2_decrypted.txt";
                public const string Text3 = Assignment3Directory + "text3_decrypted.txt";
                public const string Text4 = Assignment3Directory + "text4_decrypted.txt";
            }
        }
    }
}