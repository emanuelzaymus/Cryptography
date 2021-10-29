namespace Cryptography.RandomNumberGenerators
{
    public interface IRng
    {
        public int PeriodLength { get; }

        double Sample();

        void Restart();

        void SetSeedAndRestart(int seed);
    }
}