using System;

namespace Cryptography.RandomNumberGenerators
{
    public class OneValueRng : IRng
    {
        private readonly double _value;

        public int PeriodLength => 1;

        public OneValueRng(double value)
        {
            _value = value;
        }

        public double Sample() => _value;

        public void Restart()
        {
        }

        public void SetSeedAndRestart(int seed)
        {
            throw new InvalidOperationException($"Cannot set seed for {nameof(OneValueRng)} generator.");
        }
    }
}