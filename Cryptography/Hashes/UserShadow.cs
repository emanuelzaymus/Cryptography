namespace Cryptography.Hashes
{
    public record UserShadow(string Login, string Salt, string PasswordHash)
    {
        public string CrackedPassword { get; set; }

        public override string ToString()
        {
            return $"{Login}:{Salt}:{PasswordHash} => {CrackedPassword}";
        }
    }
}