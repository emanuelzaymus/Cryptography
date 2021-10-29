namespace Cryptography.Utilities
{
    // TODO: create Paths structure and transfer all paths here from Texts (internal)
    public static class Paths
    {
        private const string Resources = "Resources/";

        public const string Assignment3Text1Decrypted = "Resources/Assignment3/text1_decrypted.txt";
        public const string Assignment3Text2Decrypted = "Resources/Assignment3/text2_decrypted.txt";
        public const string Assignment3Text3Decrypted = "Resources/Assignment3/text3_decrypted.txt";
        public const string Assignment3Text4Decrypted = "Resources/Assignment3/text4_decrypted.txt";

        public static class Assignment3
        {
            private const string Assignment = "sdfAssignment3/";

            public const string Text4Decrypted = Resources + Assignment + "text4_decrypted.txt";
        }
    }
}