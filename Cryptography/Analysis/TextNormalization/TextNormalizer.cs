namespace Cryptography.Analysis.TextNormalization
{
    public class TextNormalizer : ITextNormalizer
    {
        private readonly Casing _casing;

        public TextNormalizer(Casing casing)
        {
            _casing = casing;
        }

        public string Normalize(string text)
        {
            return _casing is not null
                ? _casing.Transform(text)
                : text;
        }
    }
}