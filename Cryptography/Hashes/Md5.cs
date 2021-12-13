using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography.Hashes
{
    public class Md5 : IDisposable
    {
        public const int OutputByteArrayLength = 16;

        private readonly MD5 _md5 = MD5.Create();

        private readonly byte[] _helpByteArray;

        /// <summary>
        /// Creates wrapper class for <c>System.Security.Cryptography.MD5</c> with efficient management of converting and concatenating the word and the salt. 
        /// </summary>
        /// <param name="helpByteArraySize">Size of help buffer needed concatenation of the word and the salt</param>
        public Md5(int helpByteArraySize)
        {
            _helpByteArray = new byte[helpByteArraySize];
        }

        /// <summary>
        /// Converts to byte array and hashes <paramref name="wordChars"/> concatenated with <paramref name="saltBytes"/>
        /// into <paramref name="outputByteArray"/> using <c>System.Security.Cryptography.MD5</c>.
        /// </summary>
        /// <param name="wordChars">Characters of the word</param>
        /// <param name="saltBytes">Bytes of the salt</param>
        /// <param name="outputByteArray">Output array where the result will be stored from zero-index</param>
        public void ComputeHash(char[] wordChars, byte[] saltBytes, byte[] outputByteArray)
        {
            Span<byte> helpByteArray = stackalloc byte[wordChars.Length + saltBytes.Length];

            int numberOfEncodedBytes = Encoding.UTF8.GetBytes(wordChars, helpByteArray);

            for (var i = 0; i < saltBytes.Length; i++)
            {
                helpByteArray[numberOfEncodedBytes + i] = saltBytes[i];
            }

            // Array.Copy(saltBytes, 0, _helpByteArray, numberOfEncodedBytes, saltBytes.Length);

            // helByteArraySize += saltBytes.Length;

            // _md5.TransformBlock(_helpByteArray, 0, helByteArraySize, outputByteArray, 0);
            // _md5.TryComputeHash(_helpByteArray, outputByteArray, out _);
            _md5.TryComputeHash(helpByteArray, outputByteArray, out _);
        }

        public void Dispose()
        {
            _md5.Dispose();
        }
    }
}