namespace Cryptography.RandomNumberGenerators
{
    public class LinearCongruentialRng : IRng
    {
        private readonly int _a;
        private readonly int _b;
        private readonly int _m;

        private int _seed;
        private long _lastValue;

        public int PeriodLength => _m;

        public LinearCongruentialRng(int a, int b, int m, int seed = 0)
        {
            _a = a;
            _b = b;
            _m = m;

            SetSeedAndRestart(seed);
        }

        public double Sample()
        {
            _lastValue = (_lastValue * _a + _b) % _m;
            return _lastValue / (double) _m;
        }

        public void Restart()
        {
            _lastValue = _seed;
        }

        public void SetSeedAndRestart(int seed)
        {
            _seed = seed;
            Restart();
        }
    }
}