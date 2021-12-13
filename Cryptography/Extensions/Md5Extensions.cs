using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography.Extensions
{
    public static class Md5Extensions
    {
        /// <summary>
        /// Converts to byte array and hashes <paramref name="wordChars"/> concatenated with <paramref name="saltBytes"/>
        /// into <paramref name="outputByteArray"/> using this <c>System.Security.Cryptography.MD5</c>.
        /// </summary>
        /// <param name="md5">This MD5 object</param>
        /// <param name="wordChars">Characters of the word</param>
        /// <param name="saltBytes">Bytes of the salt</param>
        /// <param name="outputByteArray">Output array where the result will be stored from zero-index</param>
        public static void ComputeHash(this MD5 md5, char[] wordChars, byte[] saltBytes, byte[] outputByteArray)
        {
            Span<byte> helpByteArray = stackalloc byte[wordChars.Length + saltBytes.Length];

            int numberOfEncodedBytes = Encoding.UTF8.GetBytes(wordChars, helpByteArray);

            for (var i = 0; i < saltBytes.Length; i++)
            {
                helpByteArray[numberOfEncodedBytes + i] = saltBytes[i];
            }

            md5.TryComputeHash(helpByteArray, outputByteArray, out _);
        }
    }
}