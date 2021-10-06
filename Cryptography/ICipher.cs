namespace Cryptography
{
    public interface ICipher
    {
        string Encrypt(string text);

        string Decrypt(string text);
    }
}
