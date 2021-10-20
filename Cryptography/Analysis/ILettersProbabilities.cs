using System.Collections.Generic;

namespace Cryptography.Analysis
{
    public interface ILettersProbabilities
    {
        Dictionary<char, double> GetLettersProbabilities();
    }
}