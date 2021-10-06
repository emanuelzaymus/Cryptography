namespace Cryptography.Abstraction
{
    public interface ICipher
    {
        string Alphabet { get; }

        string Encrypt(string text);

        string Decrypt(string text);
    }
}