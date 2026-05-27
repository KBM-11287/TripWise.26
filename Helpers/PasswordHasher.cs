using System.Security.Cryptography;
using System.Text;

namespace TripWise.Api.Helpers
{
    public class PasswordHasher
    {
        public static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = Hash(password);
            return StringComparer.Ordinal.Compare(hashOfInput, hashedPassword) == 0;
        }
    }
}
