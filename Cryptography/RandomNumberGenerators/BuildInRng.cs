using System;

namespace Cryptography.RandomNumberGenerators
{
    public class BuildInRng : IRng
    {
        private readonly int _seed;
        private Random _random;

        public BuildInRng(int seed)
        {
            _seed = seed;
            _random = new Random(seed);
        }

        public double Sample()
        {
            return _random.NextDouble();
        }

        public void Restart()
        {
            _random = new Random(_seed);
        }
    }
}