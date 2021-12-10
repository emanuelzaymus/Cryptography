namespace Cryptography.Hashes
{
    public record Md5PermutationsAttack
    {
        private readonly string _alphabet;
        private readonly int _minLength;
        private readonly int _maxLength;

        public Md5PermutationsAttack(string alphabet, int minLength, int maxLength)
        {
            _alphabet = alphabet;
            _minLength = minLength;
            _maxLength = maxLength;
        }
    }
}