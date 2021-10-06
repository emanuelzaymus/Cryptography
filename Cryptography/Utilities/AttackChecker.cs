namespace Cryptography.Utilities
{
    public class AttackChecker
    {
        private readonly string _originalText;

        public AttackChecker(string originalText)
        {
            _originalText = originalText;
        }

        public bool IsDecryptedCorrectly(string decryptedText)
        {
            return _originalText == decryptedText;
        }
    }
}