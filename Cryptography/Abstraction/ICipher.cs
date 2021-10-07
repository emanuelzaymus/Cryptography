namespace Cryptography.Abstraction
{
    public interface ICipher
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}