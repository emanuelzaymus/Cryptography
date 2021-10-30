namespace Cryptography.RandomNumberGenerators
{
    /// <summary>
    /// Random number generator.
    /// </summary>
    public interface IRng
    {
        public int PeriodLength { get; }

        double Sample();

        void Restart();

        void SetSeedAndRestart(int seed);
    }
}