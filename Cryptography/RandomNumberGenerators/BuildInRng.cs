using System;

namespace Cryptography.RandomNumberGenerators
{
    public class BuildInRng : IRng
    {
        private int _seed;
        private Random _random;

        public int PeriodLength =>
            throw new InvalidOperationException($"{nameof(PeriodLength)} is not supported for {nameof(BuildInRng)}");

        public BuildInRng(int seed)
        {
            SetSeedAndRestart(seed);
        }

        public double Sample()
        {
            return _random.NextDouble();
        }

        public void Restart()
        {
            _random = new Random(_seed);
        }

        public void SetSeedAndRestart(int seed)
        {
            _seed = seed;
            Restart();
        }
    }
}