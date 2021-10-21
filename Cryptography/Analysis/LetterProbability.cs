namespace Cryptography.Analysis
{
    public readonly struct LetterProbability
    {
        public char Letter { get; }
        public double Probability { get; }

        public LetterProbability(char letter, double probability)
        {
            Letter = letter;
            Probability = probability;
        }
    }
}