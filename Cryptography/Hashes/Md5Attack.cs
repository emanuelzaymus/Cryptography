using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Hashes
{
    public abstract class Md5Attack
    {
        protected const int OutputByteArrayLength = 16;

        public IEnumerable<UserShadow> CrackPasswords(IEnumerable<UserShadow> userShadows,
            bool finishAfterFirstCracked = false)
        {
            int i = 1;
            foreach (var userShadow in userShadows)
            {
                Console.WriteLine($"{i++}. {userShadow.Login}");

                var success = TryCrackPassword(userShadow.PasswordHash, userShadow.Salt, out var crackedPassword);

                if (success)
                {
                    userShadow.CrackedPassword = crackedPassword;

                    yield return userShadow;

                    if (finishAfterFirstCracked)
                    {
                        yield break;
                    }
                }
            }
        }

        public abstract bool TryCrackPassword(string passwordHash, string salt, out string crackedPassword);

        protected byte[] HashToByteArray(string passwordHash) => Convert.FromBase64String(passwordHash);

        protected byte[] StringToByteArray(string str) => Encoding.UTF8.GetBytes(str);
    }
}