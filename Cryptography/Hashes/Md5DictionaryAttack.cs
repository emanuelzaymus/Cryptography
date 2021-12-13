using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Cryptography.Hashes
{
    public class Md5DictionaryAttack : Md5Attack
    {
        private readonly MD5 _md5 = MD5.Create();

        private readonly IReadOnlyList<string> _dictionary;

        public Md5DictionaryAttack(IReadOnlyList<string> dictionary)
        {
            _dictionary = dictionary;
        }

        public override bool TryCrackPassword(string passwordHash, string salt, out string crackedPassword)
        {
            byte[] passwordHashBytes = HashToByteArray(passwordHash);
            byte[] saltBytes = StringToByteArray(salt);

            var maxWordLength = _dictionary.Max(w => w.Length);
            var md5 = new Md5(maxWordLength + saltBytes.Length);
            var hashBytes = new byte[Md5.OutputByteArrayLength];

            foreach (var word in _dictionary)
            {
                // Creating a new char array by calling word.ToCharArray(). 
                var permutation = OneUpperCaseLetterPermutation(word.ToCharArray());

                foreach (var wordChars in permutation)
                {
                    // byte[] hash = ComputeHash(wordChars, saltBytes, _md5);
                    md5.ComputeHash(wordChars, saltBytes, hashBytes);

                    if (hashBytes.SequenceEqual(passwordHashBytes))
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
    }
}