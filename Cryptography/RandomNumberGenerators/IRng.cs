namespace Cryptography.RandomNumberGenerators
{
    public interface IRng
    {
        double Sample();

        void Restart();
    }
}