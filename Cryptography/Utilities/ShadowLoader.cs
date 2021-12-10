using System.Collections.Generic;
using System.IO;
using Cryptography.Hashes;

namespace Cryptography.Utilities
{
    public static class ShadowLoader
    {
        private const char Separator = ':';

        public static IEnumerable<UserShadow> LoadUserShadows(string filePath)
        {
            var userShadows = File.ReadAllLines(filePath);

            foreach (var shadowData in userShadows)
            {
                var userShadow = shadowData.Split(Separator);

                yield return new UserShadow(userShadow[0], userShadow[1], userShadow[2]);
            }
        }
    }
}