namespace Cryptography.Utilities
{
    public class AttackChecker
    {
        private readonly object _originalMessage;

        public AttackChecker(object originalMessage)
        {
            _originalMessage = originalMessage;
        }

        public bool IsDecryptedCorrectly(object decryptedMessage)
        {
            return Equals(_originalMessage, decryptedMessage);
        }
    }
}