﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography.Hashes
{
    public abstract class Md5Attack
    {
        private readonly MD5 _md5 = MD5.Create();

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

        public abstract bool TryCrackPassword(string passwordHash, string salt, out string crackedPassword);

        protected byte[] ComputeHash(char[] wordChars, byte[] saltBytes)
        {
            // Creates every time a new byte array.
            byte[] wordBytes = CharArrayToByteArray(wordChars);

            // Another byte array created.
            var concatenated = wordBytes.Concat(saltBytes).ToArray();

            // New byte array created again.
            return _md5.ComputeHash(concatenated);
        }

        protected byte[] HashToByteArray(string passwordHash) => Convert.FromBase64String(passwordHash);

        protected byte[] StringToByteArray(string str) => Encoding.UTF8.GetBytes(str);

        private byte[] CharArrayToByteArray(char[] charArray) => Encoding.UTF8.GetBytes(charArray);
    }
}