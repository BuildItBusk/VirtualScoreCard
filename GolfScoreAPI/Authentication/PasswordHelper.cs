using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GolfScoreAPI.Authentication
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password, string salt, int iterations = 100000, int keySize = 32)
        {
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);

            // derive a 256-bit subkey
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: iterations,
                numBytesRequested: keySize));

            return hashed;
        }

        public static bool IsMatch(string password, string hashedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
