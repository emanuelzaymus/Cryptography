namespace Cryptography.Ciphers
{
    public interface ICipher
    {
        /// <summary>
        /// Encrypts <paramref name="plainText"/> with concrete cipher.
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <returns>Encrypted text</returns>
        string Encrypt(string plainText);

        /// <summary>
        /// Decrypts <paramref name="encryptedText"/> with concrete cipher.
        /// </summary>
        /// <param name="encryptedText">Text to be decrypted</param>
        /// <returns>Plain text</returns>
        string Decrypt(string encryptedText);
    }
}