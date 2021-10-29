using Cryptography.Alphabet;

namespace Cryptography.Ciphers
{
    public abstract class Attack
    {
        protected string Alphabet { get; }

        protected Attack(string alphabet)
        {
            Alphabets.CheckAlphabet(alphabet);

            Alphabet = alphabet;
        }
    }
}