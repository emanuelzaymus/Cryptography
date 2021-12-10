using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography.Hashes
{
    public class Md5DictionaryAttack
    {
        private readonly MD5 _md5 = MD5.Create();

        private readonly IReadOnlyList<string> _dictionary;

        public Md5DictionaryAttack(IReadOnlyList<string> dictionary)
        {
            _dictionary = dictionary;
        }

        public IEnumerable<UserShadow> CrackPasswords(IEnumerable<UserShadow> userShadows)
        {
            foreach (var userShadow in userShadows)
            {
                var success = TryCrackPassword(userShadow.PasswordHash, userShadow.Salt, out var crackedPassword);

                if (success)
                {
                    userShadow.CrackedPassword = crackedPassword;

                    yield return userShadow;
                }
            }
        }

        public bool TryCrackPassword(string passwordHash, string salt, out string crackedPassword)
        {
            byte[] passwordHashBytes = Convert.FromBase64String(passwordHash);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            foreach (var word in _dictionary)
            {
                var permutation = OneUpperCaseLetterPermutation(word.ToCharArray());

                foreach (var wordChars in permutation)
                {
                    byte[] hash = ComputeHash(wordChars, saltBytes);

                    if (hash.SequenceEqual(passwordHashBytes))
                    {
                        crackedPassword = new string(wordChars);
                        return true;
                    }
                }
            }

            crackedPassword = null;
            return false;
        }

        /// <summary>
        /// Always returns the same char array <paramref name="charArray"/>.
        /// </summary>
        private IEnumerable<char[]> OneUpperCaseLetterPermutation(char[] charArray)
        {
            yield return charArray;

            for (int i = 0; i < charArray.Length; i++)
            {
                if (i - 1 >= 0)
                {
                    charArray[i - 1] = char.ToLower(charArray[i - 1]);
                }

                charArray[i] = char.ToUpper(charArray[i]);

                yield return charArray;
            }
        }

        private byte[] ComputeHash(char[] wordChars, byte[] saltBytes)
        {
            //  TODO: creates new array
            byte[] wordBytes = Encoding.UTF8.GetBytes(wordChars);

            //  TODO: creates new array
            var concatenated = wordBytes.Concat(saltBytes).ToArray();

            // TODO: TryComputeHash
            return _md5.ComputeHash(concatenated);
        }
    }
}